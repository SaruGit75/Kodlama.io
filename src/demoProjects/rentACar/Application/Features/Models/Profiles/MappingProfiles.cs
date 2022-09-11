using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, ModelListDto>()
            .ForMember(t => t.BrandName, opt => opt.MapFrom(t => t.Brand.Name)).ReverseMap();
        //formember ile dto daki brandname  i mapleyemeyeceği için gidip db modelin içindeki brand den mapledik.
        CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();
    }
}