using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;

namespace Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public async Task ProgrammingLanguageNamesCanNotBeDuplicatedWhenAdded(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(t => t.Name == name);
        if (result.Items.Any()) throw new BusinessException("Programming Language is already exist");
    }

    public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage language)
    {
        if (language == null) throw new BusinessException("Requested Language does not exists");
    }
}