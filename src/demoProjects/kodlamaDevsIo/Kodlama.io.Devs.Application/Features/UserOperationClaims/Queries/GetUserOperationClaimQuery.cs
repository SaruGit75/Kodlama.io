using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Queries;

public class GetUserOperationClaimQuery : IRequest<GetUserOperationClaimListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetUserOperationClaimQueryHandler : IRequestHandler<GetUserOperationClaimQuery, GetUserOperationClaimListModel>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;

        public GetUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetUserOperationClaimListModel> Handle(GetUserOperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaim = await _userOperationClaimRepository.GetListAsync(
                include: t => t
                    .Include(x => x.User)
                    .Include(x => x.OperationClaim),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );

            return _mapper.Map<GetUserOperationClaimListModel>(userOperationClaim);
        }
    }
}