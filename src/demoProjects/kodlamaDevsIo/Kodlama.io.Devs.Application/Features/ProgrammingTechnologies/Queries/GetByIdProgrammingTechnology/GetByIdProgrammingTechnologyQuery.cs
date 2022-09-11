using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Queries.GetByIdProgrammingTechnology;

public class GetByIdProgrammingTechnologyQuery : IRequest<ProgrammingTechnologyGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdProgrammingTechnologyQueryHandler : IRequestHandler<GetByIdProgrammingTechnologyQuery, ProgrammingTechnologyGetByIdDto>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingTechnologyBusinessRules _businessRules;

        public GetByIdProgrammingTechnologyQueryHandler(IProgrammingTechnologyRepository repository, IMapper mapper, ProgrammingTechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<ProgrammingTechnologyGetByIdDto> Handle(GetByIdProgrammingTechnologyQuery request, CancellationToken cancellationToken)
        {
            ProgrammingTechnology? technology = await _repository.GetAsync(t => t.Id == request.Id);

            _businessRules.ProgrammingTechnologyShouldExistWhenRequested(technology);

            ProgrammingTechnologyGetByIdDto technologyGetByIdDto =
                _mapper.Map<ProgrammingTechnologyGetByIdDto>(technology);

            return technologyGetByIdDto;
        }
    }
}