using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Application.CustomExceptions;

namespace Users.Application.Filter
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            switch (exception)
            {
                case UserNotFoundException e:
                    httpContext.Response.StatusCode = 404;
                    problemDetails.Title = e.Message;
                    break;

                case UserConflictException e:
                    httpContext.Response.StatusCode = 409;
                    problemDetails.Title = e.Message;
                    break;

                default:
                    httpContext.Response.StatusCode = 500;
                    problemDetails.Title = "Errore non gestito";
                    break;
            }
            logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
            problemDetails.Status = httpContext.Response.StatusCode;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
