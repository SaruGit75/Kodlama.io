using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Kodlama.io.Devs.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.Authentication.Rules;

public class AuthenticationBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthenticationBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> IsUserExist(string email)
    {
        User? isExistUser = await _userRepository.Query().AsNoTracking().FirstOrDefaultAsync(t => t.Email == email);

        return isExistUser != null
            ? isExistUser
            : throw new BusinessException("User is not found. Please check your email or register firstly.");
    }

    public async Task UserNotDuplicatedWhenRegister(string email)
    {
        User? isExistUserWhenRegister =
            await _userRepository.Query().AsNoTracking().FirstOrDefaultAsync(t => t.Email == email);

        if (isExistUserWhenRegister != null)
            throw new BusinessException("User is already register with this email. Please Login!");
    }

    public async Task IfRegisteredUserLoginCheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var check = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);

        if (!check)
            throw new BusinessException("Password not correct.");
    }
}