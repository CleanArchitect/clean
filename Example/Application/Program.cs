using Clean.Net;
using Example.Application;
using Example.Data;
using Example.Domain;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddFluentValidationRulesToSwagger()
    .AddFluentValidationAutoValidation()
    .AddControllers();

builder.Services
    .AddData(builder.Configuration.GetConnectionString("Example"))
    .AddDomain();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI();

app
    .MapGroup("/minimal/examples")
    .ToExamples()
    .WithInputValidation();

app.MapControllers();

app.Run();
