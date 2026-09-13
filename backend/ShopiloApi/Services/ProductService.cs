using ShopiloApi.Exceptions;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public Task<List<Product>> GetAllProductsAsync(
        string? search,
        int? categoryId,
        string? categorySlug,
        CancellationToken cancellationToken)
    {
        return _productRepository.GetAllAsync(search, categoryId, categorySlug, cancellationToken);
    }

    public async Task<Product> GetProductByIdAsync(
        int productId,
        CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        return product ?? throw new ProductNotFoundException(productId);
    }
}
