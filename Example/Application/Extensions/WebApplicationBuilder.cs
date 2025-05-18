using Clean.Net;
using Example.Domain;
using Example.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
            .AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseOutboundParameterTransformer())));

        return builder;
    }
}