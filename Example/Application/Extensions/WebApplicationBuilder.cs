using Clean.Net;
using Example.Domain;
using Example.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Example.Application;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetAppSettings<AppSettings>();

        builder.Services
            .AddInfrastructure(appSettings.ConnectionString)
            .AddDomain()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddFluentValidationAutoValidation()
            .AddControllers();

        return builder;
    }
}