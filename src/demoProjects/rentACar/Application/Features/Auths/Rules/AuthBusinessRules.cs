using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCannotBeDuplicatedWhenRegistered(string email)
    {
        User? user = await _userRepository.GetAsync(t => t.Email == email);
        if (user != null) throw new BusinessException("Email address already exists!");
    }
 }