using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimDto>
{
    public int Id { get; set; }
    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimsBusinessRules _operationClaimsBusinessRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimsBusinessRules operationClaimsBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimsBusinessRules = operationClaimsBusinessRules;
        }

        public async Task<DeleteOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var ifExistDeletedOperationClaim = await _operationClaimsBusinessRules.MustBeExistDeleteToEntityBeforeDeleted(request.Id);

            OperationClaim deleteToEntity = _mapper.Map<OperationClaim>(ifExistDeletedOperationClaim);
            OperationClaim deletedEntity = await _operationClaimRepository.DeleteAsync(deleteToEntity);

            return _mapper.Map<DeleteOperationClaimDto>(deletedEntity);
        }
    }
}