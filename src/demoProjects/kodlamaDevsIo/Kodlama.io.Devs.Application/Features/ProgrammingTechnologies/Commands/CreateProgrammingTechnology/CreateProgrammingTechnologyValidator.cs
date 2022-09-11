using FluentValidation;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;

public class CreateProgrammingTechnologyValidator : AbstractValidator<CreatedProgrammingLanguageDto>
{
    public CreateProgrammingTechnologyValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.Name).MinimumLength(2);
    }
}