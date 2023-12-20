using FluentValidation;

namespace Example.Domain;

internal sealed class CreateExampleInputValidator : AbstractValidator<CreateExampleInput>
{
    public CreateExampleInputValidator()
    {
        RuleFor(input => input.Name).NotEmpty();
    }
}