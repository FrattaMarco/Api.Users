using Serilog;
using System.Security.Cryptography.X509Certificates;
using Users.Application;
using Users.Application.Filter;
using Users.Extensions;
using Users.Middleware;
using Users.Persistence;

var builder = WebApplication.CreateBuilder(args);

SerilogHelper.ConfigureLogging(builder.Configuration);
// Add services to the container.
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.AddControllers();

if (builder.Environment.EnvironmentName == "Docker")
{
    string certificateName = Environment.GetEnvironmentVariable("CertName")!;
    string certificatePassword = Environment.GetEnvironmentVariable("CertPassword")!;
    builder.WebHost.UseUrls("https://*:446");
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureHttpsDefaults(listenOptions =>
        {
            listenOptions.ServerCertificate = new X509Certificate2($"{certificateName}.pfx", certificatePassword);
        });
    });
}

// Configurazione di Swagger
builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog();
builder.Services.AddHeaderPropagation(options =>
{
    options.Headers.Add("X-Correlation-ID");
});

var app = builder.Build();
app.UseExceptionHandler();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.Users v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHealthChecks("/health");
app.Run();

