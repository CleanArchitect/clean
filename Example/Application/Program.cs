using Example.Application;

WebApplication
    .CreateBuilder()
    .AddServices()
    .Build()
    .UseServices()
    .MapEndpoints()
    .Run();