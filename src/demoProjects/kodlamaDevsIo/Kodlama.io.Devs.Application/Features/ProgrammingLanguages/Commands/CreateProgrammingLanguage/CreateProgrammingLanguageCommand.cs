using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageDto>
{
    public string Name { get; set; }

    public class
        CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand,
            CreatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.ProgrammingLanguageNamesCanNotBeDuplicatedWhenAdded(request.Name);

            ProgrammingLanguage mappedLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage createdProgrammingLang = await _repository.AddAsync(mappedLanguage);

            CreatedProgrammingLanguageDto createdProgrammingLangDto =
                _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLang);

            return createdProgrammingLangDto;
        }
    }
}