using Clean.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Extensions;

public sealed class IConfigurationTests
{
    [Fact]
    public void GetAppSettings_Should_Bind_Correctly()
    {
        // arrange
        var expectedRequiredProperty = "required";
        var expectedOptionalProperty = "optional";
        var configurationManager = CreateTestConfiguration(expectedRequiredProperty, expectedOptionalProperty);

        // act
        var appSettings = configurationManager.GetAppSettings<TestAppSettings>();

        // assert
        Assert.NotNull(appSettings);
        Assert.Equal(expectedRequiredProperty, appSettings.RequiredProperty);
        Assert.Equal(expectedOptionalProperty, appSettings.OptionalProperty);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetAppSettings_Throws_Exception_When_Validation_Fails(string requiredProperty)
    {
        // arrange
        var configurationManager = CreateTestConfiguration(requiredProperty);

        // act & assert
        var exception = Assert.Throws<ValidationException>(() => configurationManager.GetAppSettings<TestAppSettings>());

        Assert.Contains(nameof(TestAppSettings.RequiredProperty), exception.Message);
    }

    private static IConfigurationRoot CreateTestConfiguration(string requiredProperty, string optionalProperty = null)
    {
        var appSettingsJson = new TestAppSettings(requiredProperty, optionalProperty);

        return new ConfigurationBuilder()
            .AddJsonStream(appSettingsJson.ToJsonMemoryStream())
            .Build();
    }

    private class TestAppSettings(string requiredProperty, string optionalProperty = null) : Settings
    {
        [Required]
        public string RequiredProperty { get; private set; } = requiredProperty;

        public string OptionalProperty { get; private set; } = optionalProperty;

        public MemoryStream ToJsonMemoryStream() =>
            new(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, Formatting.Indented)));
    }
}