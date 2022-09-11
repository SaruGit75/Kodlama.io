using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;

public class GithubProfileBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IGithubProfileRepository _githubProfileRepository;

    public GithubProfileBusinessRules(IUserRepository userRepository, IGithubProfileRepository githubProfileRepository)
    {
        _userRepository = userRepository;
        _githubProfileRepository = githubProfileRepository;
    }

    public async Task UserMustExistBeforeAddedGithubProfile(int userId)
    {
        var userIsExist = await _userRepository.Query().AsNoTracking().FirstOrDefaultAsync(t => t.Id == userId) != null;
        if (!userIsExist)
            throw new BusinessException("User cannot found!");

    }

    public async Task GithubProfileMustNotAddedOnce(int userId)
    {
        var githubProfileIsExist = await _githubProfileRepository.Query().AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == userId) == null;

        if (!githubProfileIsExist)
            throw new BusinessException("Same Github profile is already exist in records. Please check it!");
    }

    public async Task<GithubProfile> GithubProfileMustExistForDelete(int userId)
    {
        var githubProfile = await _githubProfileRepository.Query().AsNoTracking().FirstOrDefaultAsync(t => t.UserId == userId);

        return githubProfile != null ? githubProfile : throw new BusinessException("Gitub Profile not exist!");
    }

    public async Task GithubProfileMustExistWhenRequested(GithubProfile profile)
    {
        if (profile == null)
            throw new BusinessException("Github Profile is not exist.");
    }

    public async Task GithubProfileCannotBeDuplicatedWhenInsertOrUpdated(string githubProfileUrl)
    {
        var updatedEntity = await _githubProfileRepository.Query().AsNoTracking()
            .FirstOrDefaultAsync(t => t.GithubProfileUrl == githubProfileUrl);

        if (updatedEntity != null)
            throw new BusinessException("Requested Github Url is already exist!");
    }
}