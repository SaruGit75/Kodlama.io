using Core.Persistence.Repositories;
using Kodlama.io.Devs.Domain.Entities;

namespace Kodlama.io.Devs.Application.Services.Repositories;

public interface IGithubProfileRepository : IAsyncRepository<GithubProfile>, IRepository<GithubProfile>
{
    
}