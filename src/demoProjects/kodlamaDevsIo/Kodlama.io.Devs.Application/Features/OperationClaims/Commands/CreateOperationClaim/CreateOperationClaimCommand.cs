using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<CreateOperationClaimDto>
{
    public string Name { get; set; }

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimsBusinessRules _operationClaimsBusinessRules;
        private readonly IMapper _mapper;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, OperationClaimsBusinessRules operationClaimsBusinessRules, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimsBusinessRules = operationClaimsBusinessRules;
            _mapper = mapper;
        }

        public async Task<CreateOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimsBusinessRules.NotDuplicatedOperationClaimWhenInserted(request.Name);

            OperationClaim addedOperationClaim = _mapper.Map<OperationClaim>(request);

            OperationClaim operationClaim = await _operationClaimRepository.AddAsync(addedOperationClaim);
            CreateOperationClaimDto createOperationClaimDto = _mapper.Map<CreateOperationClaimDto>(operationClaim);

            return createOperationClaimDto;
        }
    }
}