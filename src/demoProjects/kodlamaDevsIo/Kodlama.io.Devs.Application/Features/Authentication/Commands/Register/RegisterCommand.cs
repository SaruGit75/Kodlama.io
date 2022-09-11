using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Authentication.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : IRequest<AccessToken>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationBusinessRules _authenticationBusinessRules;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;


        public RegisterCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository, AuthenticationBusinessRules authenticationBusinessRules, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userRepository = userRepository;
            _authenticationBusinessRules = authenticationBusinessRules;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authenticationBusinessRules.UserNotDuplicatedWhenRegister(request.UserForRegisterDto.Email);

            byte[] hashedPassword;
            byte[] saltedPassword;

            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out hashedPassword, out saltedPassword);

            User user = _mapper.Map<User>(request.UserForRegisterDto, opt =>
            {
                opt.Items.Add("PasswordHash", hashedPassword);
                opt.Items.Add("PasswordSalt", saltedPassword);
            });
            await _userRepository.AddAsync(user);


            var userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                predicate: t => t.UserId == user.Id,
                include: i => i.Include(o => o.OperationClaim)
            );

            var operationClaims = userOperationClaims.Items.Select(t => t.OperationClaim).ToList();
            var accessToken = _tokenHelper.CreateToken(user, operationClaims);

            return accessToken;
        }
    }
}