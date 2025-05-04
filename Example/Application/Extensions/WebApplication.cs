using Clean.Net;

namespace Example.Application;

internal static class WebApplicationExtensions
{
    public static WebApplication UseServices(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app
            .MapGroup("/minimal/examples")
            .ToExamples()
            .WithInputValidation();

        return app;
    }
}