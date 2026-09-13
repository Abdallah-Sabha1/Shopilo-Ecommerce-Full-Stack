using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
}
