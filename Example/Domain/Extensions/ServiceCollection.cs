using Clean.Net;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Domain;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services) =>
        services
            .AddCleanDomain()
            .AddServiceImplementations(typeof(IValidator<>));
}