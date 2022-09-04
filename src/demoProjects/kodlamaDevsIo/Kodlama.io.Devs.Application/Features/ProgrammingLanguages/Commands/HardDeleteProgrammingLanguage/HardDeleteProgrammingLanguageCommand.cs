using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.HardDeleteProgrammingLanguage;

public class HardDeleteProgrammingLanguageCommand : IRequest<HardDeleteProgrammingLanguageDto>
{
    public int Id { get; set; }

    public class HardDeleteProgrammingLanguageCommandHandler : IRequestHandler<HardDeleteProgrammingLanguageCommand, HardDeleteProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _businessRules;

        public HardDeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<HardDeleteProgrammingLanguageDto> Handle(HardDeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? deleteToLang =
                _repository.Query().AsNoTracking().FirstOrDefault(t => t.Id == request.Id && !t.IsDeleted);

            _businessRules.ProgrammingLanguageShouldExistWhenRequested(deleteToLang);

            var hardDeleteToLang = await _repository.DeleteAsync(deleteToLang);

            return _mapper.Map<HardDeleteProgrammingLanguageDto>(hardDeleteToLang);
        }
    }
}