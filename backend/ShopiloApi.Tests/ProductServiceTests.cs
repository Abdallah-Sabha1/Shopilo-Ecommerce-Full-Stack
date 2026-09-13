using ShopiloApi.Exceptions;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;
using ShopiloApi.Services;
using Xunit;

namespace ShopiloApi.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_WhenProductExists_ReturnsProduct()
    {
        var expected = new Product { Id = 7, Title = "Keyboard" };
        var service = new ProductService(new FakeProductRepository(expected));

        Product result = await service.GetProductByIdAsync(7, CancellationToken.None);

        Assert.Same(expected, result);
    }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
    {
        var service = new ProductService(new FakeProductRepository(null));

        await Assert.ThrowsAsync<ProductNotFoundException>(() =>
            service.GetProductByIdAsync(99, CancellationToken.None));
    }

    private sealed class FakeProductRepository : IProductRepository
    {
        private readonly Product? _product;

        public FakeProductRepository(Product? product) => _product = product;

        public Task<List<Product>> GetAllAsync(
            string? search,
            int? categoryId,
            string? categorySlug,
            CancellationToken cancellationToken) =>
            Task.FromResult(_product is null ? new List<Product>() : new List<Product> { _product });

        public Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken) =>
            Task.FromResult(_product?.Id == productId ? _product : null);
    }
}
