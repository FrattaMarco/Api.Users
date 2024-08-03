using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Users.Application.CustomValidations;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Users.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration conf)
        {
            string connectionString = conf.GetConnectionString("ConnectionStringUsers") ?? throw new ArgumentNullException(nameof(conf));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly));
            services.AddHealthChecks();
            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            services.AddFluentValidationAutoValidation();
            #region HealthCheck
            services.AddHealthChecks().AddCheck("Api.Users Health Check", () => HealthCheckResult.Healthy("Api.Users is healthy"));

            services.Configure<HealthCheckOptions>(options =>
            {
                options.ResponseWriter = async (context, report) =>
                {
                    var result = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            description = entry.Key.Replace("Health Check", "")
                        })
                    };
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                };
            });
            #endregion
        }
    }
}
