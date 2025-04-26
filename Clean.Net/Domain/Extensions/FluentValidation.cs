using FluentValidation;

namespace Clean.Net;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<TInput, string> DigitsOnly<TInput>(this IRuleBuilder<TInput, string> builder) =>
        builder.Matches(@"^\d+$");
}