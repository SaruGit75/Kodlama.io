using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Kodlama.io.Devs.Application.Features.Authentication.Commands.Register;

namespace Kodlama.io.Devs.Application.Features.Authentication.Profiles;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<UserForRegisterDto, User>()
            .ForMember(c => c.PasswordHash, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items["PasswordHash"]))
            .ForMember(c => c.PasswordSalt, opt => opt.MapFrom((src, dest, _, ctx) => ctx.Items["PasswordSalt"]))
            .AfterMap((src, dest) =>
            {
                dest.Status = true;
                dest.AuthenticatorType = AuthenticatorType.None;
            })
            .ReverseMap();
    }
}