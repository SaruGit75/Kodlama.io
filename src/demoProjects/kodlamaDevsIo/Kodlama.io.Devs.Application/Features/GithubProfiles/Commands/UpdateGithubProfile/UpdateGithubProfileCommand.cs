using AutoMapper;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.UpdateGithubProfile;

public class UpdateGithubProfileCommand : IRequest<UpdatedGithubProfileDto>
{
    public int UserId { get; set; }
    public string GithubProfileUrl { get; set; }

    public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdatedGithubProfileDto>
    {
        private readonly IGithubProfileRepository _githubProfileRepository;
        private readonly IMapper _mapper;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

        public UpdateGithubProfileCommandHandler(IGithubProfileRepository githubProfileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
        {
            _githubProfileRepository = githubProfileRepository;
            _mapper = mapper;
            _githubProfileBusinessRules = githubProfileBusinessRules;
        }

        public async Task<UpdatedGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
        {
            GithubProfile? githubProfile = await _githubProfileRepository.Query().AsNoTracking()
                .FirstOrDefaultAsync(t => t.UserId == request.UserId);
            githubProfile.GithubProfileUrl = request.GithubProfileUrl;


            await _githubProfileBusinessRules.GithubProfileMustExistWhenRequested(githubProfile);
            await _githubProfileBusinessRules.GithubProfileCannotBeDuplicatedWhenInsertOrUpdated(
                request.GithubProfileUrl);

            GithubProfile updatedGithubProfile = await _githubProfileRepository.UpdateAsync(githubProfile);

            return _mapper.Map<UpdatedGithubProfileDto>(updatedGithubProfile);
        }
    }
}