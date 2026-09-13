using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
}
