using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Rules;

public class OperationClaimsBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimsBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task NotDuplicatedOperationClaimWhenInserted(string operationClaimName)
    {
        OperationClaim? operationClaim = _operationClaimRepository.Query().AsNoTracking()
            .FirstOrDefault(t => t.Name == operationClaimName);

        if (operationClaim != null)
            throw new BusinessException("The operation claim is already inserted.");
    }

    public async Task<OperationClaim> MustBeExistDeleteToEntityBeforeDeleted(int id)
    {
        OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(t => t.Id == id);
        if (operationClaim == null)
            throw new BusinessException("The entity you want to delete does not exist!");
        return operationClaim;
    }
}