using AutoMapper;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile;

public class CreateGithubProfileCommand : IRequest<CreatedGithubProfileDto>
{
    public int UserId { get; set; }
    public string GithubProfileUrl { get; set; }

    public class CreateGithubProfileCommandHandler : IRequestHandler<CreateGithubProfileCommand, CreatedGithubProfileDto>
    {
        private readonly IGithubProfileRepository _repository;
        private readonly IMapper _mapper;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

        public CreateGithubProfileCommandHandler(IGithubProfileRepository repository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _githubProfileBusinessRules = githubProfileBusinessRules;
        }

        public async Task<CreatedGithubProfileDto> Handle(CreateGithubProfileCommand request, CancellationToken cancellationToken)
        {
            await _githubProfileBusinessRules.UserMustExistBeforeAddedGithubProfile(request.UserId);
            await _githubProfileBusinessRules.GithubProfileMustNotAddedOnce(request.UserId);

            var mappedGithubProfile = _mapper.Map<GithubProfile>(request);
            var createdGithubProfileDto = await _repository.AddAsync(mappedGithubProfile);

            return _mapper.Map<CreatedGithubProfileDto>(createdGithubProfileDto);
        }
    }
}