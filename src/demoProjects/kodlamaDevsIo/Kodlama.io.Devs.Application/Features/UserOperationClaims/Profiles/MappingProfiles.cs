using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.CreateUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaims;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Models;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<UserOperationClaim>, GetUserOperationClaimListModel>().ReverseMap();
        CreateMap<UserOperationClaim, UserOperationClaimDto>().ReverseMap();

        CreateMap<UserOperationClaim, CreateUserOperationClaimsCommand>().ReverseMap();
        CreateMap<UserOperationClaim, CreateUserOperationClaimsDto>().ReverseMap();

        CreateMap<UserOperationClaim, UpdateUserOperationClaimsCommand>().ReverseMap();
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();

        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, DeleteUserOperationClaimDto>().ReverseMap();
    }
}