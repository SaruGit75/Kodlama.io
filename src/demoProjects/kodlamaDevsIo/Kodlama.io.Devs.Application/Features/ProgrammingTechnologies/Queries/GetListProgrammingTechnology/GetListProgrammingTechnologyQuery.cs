using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology;

public class GetListProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListProgrammingTechnologyQueryHandler : IRequestHandler<GetListProgrammingTechnologyQuery, ProgrammingTechnologyListModel>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;

        public GetListProgrammingTechnologyQueryHandler(IProgrammingTechnologyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProgrammingTechnologyListModel> Handle(GetListProgrammingTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingTechnology> models = await _repository
                .GetListAsync(
                            predicate: t => !t.IsDeleted,
                            include: t => t.Include(p => p.ProgrammingLanguage), 
                            index: request.PageRequest.Page,
                            size: request.PageRequest.PageSize
                );

            ProgrammingTechnologyListModel mappedTechnologyListModel = _mapper.Map<ProgrammingTechnologyListModel>(models);

            return mappedTechnologyListModel;
        }
    }
}