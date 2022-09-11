using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.SoftDeleteProgrammingTechnologies;

public class SoftDeleteProgrammingTechnologyCommand : IRequest<SoftDeleteProgrammingTechnologyDto>
{
    public int Id { get; set; }

    public class SoftDeleteProgrammingTechnologyCommandHandler : IRequestHandler<SoftDeleteProgrammingTechnologyCommand, SoftDeleteProgrammingTechnologyDto>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingTechnologyBusinessRules _businessRules;
        public SoftDeleteProgrammingTechnologyCommandHandler(IProgrammingTechnologyRepository repository, IMapper mapper, ProgrammingTechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<SoftDeleteProgrammingTechnologyDto> Handle(SoftDeleteProgrammingTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingTechnology? deleteToTech = _repository.Query().AsNoTracking()
                .FirstOrDefault(t => t.Id == request.Id && !t.IsDeleted);

            _businessRules.ProgrammingTechnologyShouldExistWhenRequested(deleteToTech);
            deleteToTech.IsDeleted = true;

            var softDeleteToTech = await _repository.UpdateAsync(deleteToTech);

            return _mapper.Map<SoftDeleteProgrammingTechnologyDto>(softDeleteToTech);
        }
    }
}