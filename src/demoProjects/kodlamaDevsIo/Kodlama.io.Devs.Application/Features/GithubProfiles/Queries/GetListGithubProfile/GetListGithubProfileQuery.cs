using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetListGithubProfile;

public class GetListGithubProfileQuery : IRequest<GithubProfileListModel>
{
    public PageRequest PageRequest { get; set; }
    public class GetListGithubProfileQueryHandler : IRequestHandler<GetListGithubProfileQuery, GithubProfileListModel>
    {
        private readonly IGithubProfileRepository _repository;
        private readonly IMapper _mapper;

        public GetListGithubProfileQueryHandler(IGithubProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GithubProfileListModel> Handle(GetListGithubProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<GithubProfile> githubProfile = await _repository.GetListAsync(
                include: t => t.Include(i => i.User),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );

            return _mapper.Map<GithubProfileListModel>(githubProfile);
        }
    }
}