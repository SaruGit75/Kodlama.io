using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnologies;

public class UpdateProgrammingTechnologyCommand : IRequest<UpdatedProgrammingTechnologyDto>
{
    public int Id { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public class UpdateProgrammingTechnologyCommandHandler : IRequestHandler<UpdateProgrammingTechnologyCommand, UpdatedProgrammingTechnologyDto>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingTechnologyBusinessRules _businessRules;

        public UpdateProgrammingTechnologyCommandHandler(IProgrammingTechnologyRepository repository, IMapper mapper, ProgrammingTechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedProgrammingTechnologyDto> Handle(UpdateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
        {
            var updateToEntity = _repository.Query().AsNoTracking().FirstOrDefault(t => t.Id == request.Id);

            _businessRules.ProgrammingTechnologyShouldExistWhenRequested(updateToEntity);
            await _businessRules.ProgrammingTechnologyNamesCanNotBeDuplicatedWhenAdded(request.Name);

            ProgrammingTechnology? mappedTechnology = _mapper.Map<ProgrammingTechnology>(request);
            ProgrammingTechnology? updatedTechnology = await _repository.UpdateAsync(mappedTechnology);

            UpdatedProgrammingTechnologyDto updatedProgrammingTechnologyDto =
                _mapper.Map<UpdatedProgrammingTechnologyDto>(updatedTechnology);

            return updatedProgrammingTechnologyDto;
        }
    }
}