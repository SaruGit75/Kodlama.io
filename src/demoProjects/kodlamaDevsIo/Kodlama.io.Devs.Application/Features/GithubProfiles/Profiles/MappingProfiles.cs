using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.HardDeleteGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Domain.Entities;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatedGithubProfileDto, GithubProfile>().ReverseMap();
        CreateMap<CreateGithubProfileCommand, GithubProfile>().ReverseMap();
        CreateMap<HardDeleteGithubProfileCommand, GithubProfile>().ReverseMap();
        CreateMap<HardDeletedGithubProfileDto, GithubProfile>().ReverseMap();
        CreateMap<UpdatedGithubProfileDto, GithubProfile>().ReverseMap();
        CreateMap<UpdateGithubProfileCommand, GithubProfile>().ReverseMap();
        CreateMap<UpdateGithubProfileCommand, GithubProfile>().ReverseMap();
        CreateMap<IPaginate<GithubProfile>, GithubProfileListModel>().ReverseMap();

    }
}