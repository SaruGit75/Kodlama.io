using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;

namespace Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;

public class ProgrammingTechnologyBusinessRules
{
    private readonly IProgrammingTechnologyRepository _repository;
    private readonly IProgrammingLanguageRepository _languageRepository;

    public ProgrammingTechnologyBusinessRules(IProgrammingTechnologyRepository repository, IProgrammingLanguageRepository languageRepository)
    {
        _repository = repository;
        _languageRepository = languageRepository;
    }

    public async Task ProgrammingTechnologyNamesCanNotBeDuplicatedWhenAdded(string name)
    {
        IPaginate<ProgrammingTechnology> result = await _repository.GetListAsync(t => t.Name == name);
        if (result.Items.Any()) throw new BusinessException("Programming Technology is already exist");
    }

    public async Task<int> ProgrammingLanguageNameIsExistCheckBeforeAddTech(string programmingLangName)
    {
        ProgrammingLanguage? result = await _languageRepository.GetAsync(t => t.Name == programmingLangName);

        if(result != null) return result.Id;
     
        ProgrammingLanguage addedLang = await _languageRepository.AddAsync(new()
        {
            Name = programmingLangName
        });
        if (addedLang != null) return addedLang.Id;

        return -1;
    }

    public void ProgrammingTechnologyShouldExistWhenRequested(ProgrammingTechnology technology)
    {
        if (technology == null) throw new BusinessException("Requested Technology does not exists");
    }

}