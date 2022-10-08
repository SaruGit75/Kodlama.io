using Core.Persistence.Repositories;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Persistence.Contexts;

namespace Kodlama.io.Devs.Persistence.Repositories;

public class RefreshTokenRepository :EfRepositoryBase<RefreshToken, KodlamaIoContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(KodlamaIoContext context) : base(context)
    {
    }
}