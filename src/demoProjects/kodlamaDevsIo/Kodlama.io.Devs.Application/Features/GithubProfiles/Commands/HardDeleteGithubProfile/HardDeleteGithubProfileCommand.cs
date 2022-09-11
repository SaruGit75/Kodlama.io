using AutoMapper;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.HardDeleteGithubProfile;

public class HardDeleteGithubProfileCommand : IRequest<HardDeletedGithubProfileDto>
{
    public int UserId { get; set; }

    public class HardDeleteGithubProfileCommandHandler : IRequestHandler<HardDeleteGithubProfileCommand, HardDeletedGithubProfileDto>
    {
        private readonly IGithubProfileRepository _repository;
        private readonly IMapper _mapper;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;
        public HardDeleteGithubProfileCommandHandler(IGithubProfileRepository repository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _githubProfileBusinessRules = githubProfileBusinessRules;
        }

        public async Task<HardDeletedGithubProfileDto> Handle(HardDeleteGithubProfileCommand request, CancellationToken cancellationToken)
        {
            GithubProfile githubProfile =
                await _githubProfileBusinessRules.GithubProfileMustExistForDelete(request.UserId);

            var deletedEntity = await _repository.DeleteAsync(githubProfile);

            return _mapper.Map<HardDeletedGithubProfileDto>(deletedEntity);
        }
    }
}