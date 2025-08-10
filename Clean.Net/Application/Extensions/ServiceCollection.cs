using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// Registers the provided settings in the <see cref="IServiceCollection"/> and validates them using data annotations.
    /// Add data annotations validators to your settings class. 
    /// See <see href="https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations"/> for more info.
    /// </summary>
    /// <returns>The modified <see cref="IServiceCollection"/> with registered settings.</returns>
    public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, TSettings settings) where TSettings : Settings =>
        services
            .AddSingleton(typeof(TSettings), settings.Validate());
}