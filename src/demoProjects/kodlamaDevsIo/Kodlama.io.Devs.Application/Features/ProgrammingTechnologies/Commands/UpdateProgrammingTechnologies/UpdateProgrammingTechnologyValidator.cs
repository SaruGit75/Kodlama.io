using FluentValidation;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnologies;

public class UpdateProgrammingTechnologyValidator : AbstractValidator<UpdatedProgrammingTechnologyDto>
{
    public UpdateProgrammingTechnologyValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
        RuleFor(t => t.Name).MinimumLength(2);
    }

}