using System.ComponentModel.DataAnnotations;

namespace Clean.Net;

/// <summary>
/// Provides a base class for your configuration binded settings for example from appsettings.json.
/// Register them using <see cref="ApplicationServiceCollectionExtensions.AddSettings{TSettings}(Microsoft.Extensions.DependencyInjection.IServiceCollection, TSettings)"/>
/// so they get added to the <see cref="IServiceCollection"/> as <see cref="IOptions"/> and validated on
/// start of application.
/// </summary>
public abstract class Settings
{
    internal Settings Validate()
    {
        Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        return this;
    }
}