using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface ISeedRepository
{
    Task<bool> HasDataAsync(CancellationToken cancellationToken);
    Task AddCategoriesAsync(List<Category> categories, CancellationToken cancellationToken);
    Task AddProductsAsync(List<Product> products, CancellationToken cancellationToken);
}
