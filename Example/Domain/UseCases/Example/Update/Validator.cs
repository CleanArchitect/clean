using FluentValidation;

namespace Example.Domain;

internal sealed class UpdateExampleInputValidator : AbstractValidator<UpdateExampleInput>
{
    public UpdateExampleInputValidator()
    {
        RuleFor(input => input.Name).NotEmpty();
    }
}