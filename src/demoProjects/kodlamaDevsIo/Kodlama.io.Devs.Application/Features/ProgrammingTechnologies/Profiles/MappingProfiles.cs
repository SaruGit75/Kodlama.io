using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnologies;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Models;
using Kodlama.io.Devs.Domain.Entities;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProgrammingTechnologyListModel, IPaginate<ProgrammingTechnology>>().ReverseMap();
        CreateMap<ProgrammingTechnology, ProgrammingTechnologyListDto>()
            .ForMember(t => t.ProgrammingLanguageName, 
                opt => opt.MapFrom(t => t.ProgrammingLanguage.Name)).ReverseMap();


        CreateMap<ProgrammingTechnology, CreateProgrammingTechnologyCommand>().ForMember(t => t.ProgrammingLanguageName, 
            opt => opt.MapFrom(t => t.ProgrammingLanguage.Name)).ReverseMap();
        CreateMap<ProgrammingTechnology, CreatedProgrammingTechnologyDto>().ReverseMap();

        CreateMap<ProgrammingTechnology, ProgrammingTechnologyGetByIdDto>().ReverseMap();
        CreateMap<ProgrammingTechnology, HardDeleteProgrammingTechnologyDto>().ReverseMap();
        CreateMap<ProgrammingTechnology, SoftDeleteProgrammingTechnologyDto>().ReverseMap();
        CreateMap<ProgrammingTechnology, UpdatedProgrammingTechnologyDto>().ReverseMap();
        CreateMap<ProgrammingTechnology, UpdateProgrammingTechnologyCommand>().ReverseMap();
    }
}