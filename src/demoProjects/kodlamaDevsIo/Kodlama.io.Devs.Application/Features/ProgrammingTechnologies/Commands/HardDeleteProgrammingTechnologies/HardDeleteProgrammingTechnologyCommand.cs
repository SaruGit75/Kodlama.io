using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.HardDeleteProgrammingTechnologies;

public class HardDeleteProgrammingTechnologyCommand : IRequest<HardDeleteProgrammingTechnologyDto>
{
    public int Id { get; set; }

    public class HardDeleteProgrammingTechnologyCommandHandler : IRequestHandler<HardDeleteProgrammingTechnologyCommand, HardDeleteProgrammingTechnologyDto>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingTechnologyBusinessRules _businessRules;


        public HardDeleteProgrammingTechnologyCommandHandler(IProgrammingTechnologyRepository repository, IMapper mapper, ProgrammingTechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<HardDeleteProgrammingTechnologyDto> Handle(HardDeleteProgrammingTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingTechnology? deleteToTech = _repository.Query().AsNoTracking()
                .FirstOrDefault(t => t.Id == request.Id && !t.IsDeleted);

            _businessRules.ProgrammingTechnologyShouldExistWhenRequested(deleteToTech);

            var hardDeleteToTech = await _repository.DeleteAsync(deleteToTech);

            return _mapper.Map<HardDeleteProgrammingTechnologyDto>(hardDeleteToTech);
        }
    }
}