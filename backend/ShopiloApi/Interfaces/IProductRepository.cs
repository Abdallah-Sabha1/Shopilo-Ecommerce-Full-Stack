using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(
        string? search,
        int? categoryId,
        string? categorySlug,
        CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken);
}
