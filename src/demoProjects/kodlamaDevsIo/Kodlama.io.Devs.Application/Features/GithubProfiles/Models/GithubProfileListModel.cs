using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Models;

public class GithubProfileListModel : BasePageableModel
{
    public IList<GetListGithubProfileDto> Items { get; set; }
}