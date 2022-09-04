using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.SoftDeleteProgrammingLanguage;

public class SoftDeleteProgrammingLanguageCommand : IRequest<SoftDeleteProgrammingLanguageDto>
{
    public int Id { get; set; }

    public class SoftDeleteProgrammingLanguageCommandHandler : IRequestHandler<SoftDeleteProgrammingLanguageCommand,
        SoftDeleteProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public SoftDeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<SoftDeleteProgrammingLanguageDto> Handle(SoftDeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? deleteToLang = _repository.Query().AsNoTracking().FirstOrDefault(t => t.Id == request.Id && !t.IsDeleted);

            _businessRules.ProgrammingLanguageShouldExistWhenRequested(deleteToLang);

            deleteToLang.IsDeleted = true;

            var softDeleteLang = await _repository.UpdateAsync(deleteToLang);

            return _mapper.Map<SoftDeleteProgrammingLanguageDto>(softDeleteLang);
        }
    }
}