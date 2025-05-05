using Microsoft.Extensions.Configuration;

namespace Clean.Net;

public static class ConfigurationManagerExtentions
{
    /// <summary>
    /// Retrieves and binds the entire appsettings.json configuration to the specified <typeparamref name="TAppSettings"/>.
    /// The method automatically applies non-public property binding and validates the settings.
    /// </summary>
    /// <typeparam name="TAppSettings">The type of settings class to bind the configuration values to.</typeparam>
    /// <param name="configurationManager">The configuration manager instance used to retrieve settings.</param>
    /// <param name="options">An optional delegate to configure binding options.</param>
    /// <returns>An instance of <typeparamref name="TAppSettings"/> with bound and validated configuration values.</returns>
    public static TAppSettings GetAppSettings<TAppSettings>(this IConfiguration configurationManager, Action<BinderOptions> options = null) where TAppSettings : Settings =>
        configurationManager
            .Get<TAppSettings>(defaultOptions =>
            {
                defaultOptions.BindNonPublicProperties = true;
                options?.Invoke(defaultOptions);
            })
            .Validate() as TAppSettings;
}