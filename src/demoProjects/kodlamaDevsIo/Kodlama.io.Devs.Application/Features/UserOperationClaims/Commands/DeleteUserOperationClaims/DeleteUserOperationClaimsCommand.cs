using AutoMapper;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims;

public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimDto>
{
    public int Id { get; set; }
    public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository operationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeleteUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            var ifExistDeletedUserOperationClaim = await _userOperationClaimBusinessRules.MustBeExistDeleteToEntityBeforeDeleted(request.Id);

            UserOperationClaim deleteToEntity = _mapper.Map<UserOperationClaim>(ifExistDeletedUserOperationClaim);
            UserOperationClaim deletedEntity = await _operationClaimRepository.DeleteAsync(deleteToEntity);

            return _mapper.Map<DeleteUserOperationClaimDto>(deletedEntity);
        }
    }
}