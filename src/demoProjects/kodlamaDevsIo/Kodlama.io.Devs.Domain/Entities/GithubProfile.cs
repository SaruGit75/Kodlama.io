using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Kodlama.io.Devs.Domain.Entities;

public class GithubProfile : Entity
{
    public int UserId { get; set; }
    public string GithubProfileUrl { get; set; }

    public virtual User? User { get; set; }

    public GithubProfile()
    {
    }

    public GithubProfile(int id, int userId, string gitGithubProfileUrl)
    {
        Id = id;
        GithubProfileUrl = gitGithubProfileUrl;
        UserId = userId;
    }
}