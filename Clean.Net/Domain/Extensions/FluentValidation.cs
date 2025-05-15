using FluentValidation;

namespace Clean.Net;

public static class FluentValidationExtensions
{
    /// <summary>
    /// Defines a digits only validator for strings. Validation will fail if the string 
    /// contains other characters besides digits. Null is not validated.
    /// </summary>
    public static IRuleBuilderOptions<TInput, string> DigitsOnly<TInput>(this IRuleBuilder<TInput, string> builder) =>
        builder
            .Matches(@"^\d+$");
}