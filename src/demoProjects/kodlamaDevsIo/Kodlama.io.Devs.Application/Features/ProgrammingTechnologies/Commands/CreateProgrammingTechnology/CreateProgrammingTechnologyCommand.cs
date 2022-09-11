using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;

public class CreateProgrammingTechnologyCommand : IRequest<CreatedProgrammingTechnologyDto>
{
    public string Name { get; set; }
    public string ProgrammingLanguageName { get; set; }
    public class CreateProgrammingTechnologyCommandHandler : IRequestHandler<CreateProgrammingTechnologyCommand, CreatedProgrammingTechnologyDto>
    {
        private readonly IProgrammingTechnologyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingTechnologyBusinessRules _businessRules;

        public CreateProgrammingTechnologyCommandHandler(IProgrammingTechnologyRepository repository, IMapper mapper, ProgrammingTechnologyBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<CreatedProgrammingTechnologyDto> Handle(CreateProgrammingTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ProgrammingTechnologyNamesCanNotBeDuplicatedWhenAdded(request.Name);

            var languageIdOrNon = await _businessRules.ProgrammingLanguageNameIsExistCheckBeforeAddTech(request.ProgrammingLanguageName);

            if (languageIdOrNon == -1)
                throw new BusinessException("An unexpected error has occurred.");


            ProgrammingTechnology mappedProgrammingTechnology = _mapper.Map<ProgrammingTechnology>(request);

            mappedProgrammingTechnology.ProgrammingLanguageId = languageIdOrNon;
            mappedProgrammingTechnology.ProgrammingLanguage.Id = languageIdOrNon;

            ProgrammingTechnology createdProgrammingTechnology = await _repository.AddAsync(mappedProgrammingTechnology);

            CreatedProgrammingTechnologyDto createdProgrammingLanguageDto =
                _mapper.Map<CreatedProgrammingTechnologyDto>(createdProgrammingTechnology);

            return createdProgrammingLanguageDto;
        }
    }
}