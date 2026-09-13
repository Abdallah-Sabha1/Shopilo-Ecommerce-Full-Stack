using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync(
        string? search,
        int? categoryId,
        string? categorySlug,
        CancellationToken cancellationToken);
    Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
}
