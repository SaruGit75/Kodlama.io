using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaims;
using Kodlama.io.Devs.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task UserOperationClaimsMustNoDuplicatedOperationClaims(CreateUserOperationClaimsCommand request)
    {
        var isExistUserClaims = await _userOperationClaimRepository.Query().AsNoTracking().AnyAsync(t =>
            t.UserId == request.UserId && t.OperationClaimId == request.OperationClaimId);

        if (isExistUserClaims)
            throw new BusinessException("Same record already exists!");
    }

    public async Task UserOperationClaimsMustNoDuplicatedWhenUpdating(UpdateUserOperationClaimsCommand request)
    {
        var isExistUserClaims = await _userOperationClaimRepository.Query().AsNoTracking().AnyAsync(t =>
            t.UserId == request.UserId && t.OperationClaimId == request.OperationClaimId);

        if (isExistUserClaims)
            throw new BusinessException("Same record already exists!");
    }

    public async Task<UserOperationClaim> MustBeExistDeleteToEntityBeforeDeleted(int requestId)
    {
        UserOperationClaim? isExistDeleteToEntity = await _userOperationClaimRepository.GetAsync(t => t.Id == requestId);
        return isExistDeleteToEntity ?? throw new BusinessException("The file you want to delete was not found");
    }
}