using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;

public class GetByIdProgrammingLanguageQuery : IRequest<ProgrammingLanguageGetByIdDto>
{
    public int Id { get; set; }

    public class
        GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery,
            ProgrammingLanguageGetByIdDto>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _rules;

        public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository repository, IMapper mapper, ProgrammingLanguageBusinessRules rules)
        {
            _repository = repository;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? language = await _repository.GetAsync(t => t.Id == request.Id);
            _rules.ProgrammingLanguageShouldExistWhenRequested(language);

            ProgrammingLanguageGetByIdDto languageGetByIdDto = _mapper.Map<ProgrammingLanguageGetByIdDto>(language);
            return languageGetByIdDto;
        }
    }
}