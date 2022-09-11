using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Authentication.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.Authentication.Commands.Login;

public class LoginCommand : IRequest<AccessToken>
{
    public UserForLoginDto UserForLoginDto { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthenticationBusinessRules _authenticationBusinessRules;

        public LoginCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, AuthenticationBusinessRules authenticationBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
            _authenticationBusinessRules = authenticationBusinessRules;
        }

        public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _authenticationBusinessRules.IsUserExist(request.UserForLoginDto.Email);
            await _authenticationBusinessRules
                .IfRegisteredUserLoginCheckPassword(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);


            var userOperationClaims = await _userOperationClaimRepository
                .GetListAsync(
                predicate: t => t.UserId == user.Id,
                include: t => t.Include(a => a.OperationClaim)
            );

            var operationClaims = userOperationClaims.Items.Select(t => t.OperationClaim).ToList();
            var accessToken = _tokenHelper.CreateToken(user, operationClaims);

            return accessToken;
        }
    }
}