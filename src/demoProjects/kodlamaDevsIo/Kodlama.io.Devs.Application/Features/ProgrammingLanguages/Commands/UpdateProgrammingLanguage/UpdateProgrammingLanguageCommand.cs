using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            var updateToEntity = _repository.Query().AsNoTracking().FirstOrDefault(t => t.Id == request.Id);

            _businessRules.ProgrammingLanguageShouldExistWhenRequested(updateToEntity);
            await _businessRules.ProgrammingLanguageNamesCanNotBeDuplicatedWhenAdded(request.Name);

            ProgrammingLanguage mappedLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage updatedLanguage = await _repository.UpdateAsync(mappedLanguage);

            UpdatedProgrammingLanguageDto updatedLanguageDto =
                _mapper.Map<UpdatedProgrammingLanguageDto>(updatedLanguage);

            return updatedLanguageDto;
        }
    }
}