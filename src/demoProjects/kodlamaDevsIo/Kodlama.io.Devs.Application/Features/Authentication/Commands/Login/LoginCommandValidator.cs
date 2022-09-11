using FluentValidation;

namespace Kodlama.io.Devs.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(t => t.UserForLoginDto.Email).NotEmpty();
        RuleFor(t => t.UserForLoginDto.Password).NotEmpty();
        RuleFor(t => t.UserForLoginDto.Password).MinimumLength(6);
    }
}