using Clean.Net;
using Example.Domain;
using Example.Infrastructure;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

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
            .AddFluentValidationRulesToSwagger()
            .AddFluentValidationAutoValidation()
            .AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseOutputParameterTransformer())));

        return builder;
    }
}