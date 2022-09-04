using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

/// <summary>
/// Brande ait ozel operasyonlar buraya yazilacak.
/// </summary>
public interface IBrandRepository : IAsyncRepository<Brand>, IRepository<Brand>
{
}