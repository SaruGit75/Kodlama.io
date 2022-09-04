using FluentValidation;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageValidator : AbstractValidator<CreateProgrammingLanguageCommand>
{
    public CreateProgrammingLanguageValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.Name).MinimumLength(2);
    }
}