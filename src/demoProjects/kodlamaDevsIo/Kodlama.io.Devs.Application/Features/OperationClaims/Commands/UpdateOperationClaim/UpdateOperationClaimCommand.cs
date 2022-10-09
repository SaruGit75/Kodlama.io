using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Commands.UpdateOperationClaim;

public class UpdateOperationClaimCommand : IRequest<UpdateOperationClaimDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdateOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimsBusinessRules _operationClaimsBusinessRules;
        private readonly IMapper _mapper;
        public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimsBusinessRules operationClaimsBusinessRules, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimsBusinessRules = operationClaimsBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdateOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimsBusinessRules.NotDuplicatedOperationClaimWhenInserted(request.Name);

            OperationClaim updateOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(updateOperationClaim);

            return _mapper.Map<UpdateOperationClaimDto>(updatedOperationClaim);
        }
    }

}