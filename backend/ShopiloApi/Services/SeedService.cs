using System.Net.Http.Json;
using ShopiloApi.Exceptions;
using ShopiloApi.ExternalDtos;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Services;

public class SeedService : ISeedService
{
    private readonly ISeedRepository _seedRepository;
    private readonly HttpClient _httpClient;

    public SeedService(ISeedRepository seedRepository, HttpClient httpClient)
    {
        _seedRepository = seedRepository;
        _httpClient = httpClient;
    }

    public async Task<SeedResult> SeedAsync(CancellationToken cancellationToken)
    {
        if (await _seedRepository.HasDataAsync(cancellationToken))
        {
            throw new SeedAlreadyCompletedException();
        }

        List<DummyCategoryDto>? sourceCategories = await _httpClient
            .GetFromJsonAsync<List<DummyCategoryDto>>(
                "https://dummyjson.com/products/categories",
                cancellationToken);

        if (sourceCategories is null || sourceCategories.Count == 0)
        {
            throw new ExternalDataException("The external API did not return categories.");
        }

        List<Category> categories = sourceCategories.Select(category => new Category
        {
            Name = category.Name,
            Slug = category.Slug
        }).ToList();

        await _seedRepository.AddCategoriesAsync(categories, cancellationToken);

        DummyProductListResponse? sourceProducts = await _httpClient
            .GetFromJsonAsync<DummyProductListResponse>(
                "https://dummyjson.com/products?limit=0",
                cancellationToken);

        if (sourceProducts?.Products is null)
        {
            throw new ExternalDataException("The external API did not return products.");
        }

        Dictionary<string, int> categoryIds = categories.ToDictionary(
            category => category.Slug,
            category => category.Id,
            StringComparer.OrdinalIgnoreCase);

        List<Product> products = sourceProducts.Products
            .Where(product => categoryIds.ContainsKey(product.Category))
            .Select(product => new Product
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                Rating = product.Rating,
                Stock = product.Stock,
                Brand = product.Brand,
                Thumbnail = product.Thumbnail,
                CategoryId = categoryIds[product.Category],
                ProductImages = product.Images?.Select(image => new ProductImage
                {
                    Url = image
                }).ToList() ?? new(),
                ProductTags = product.Tags?.Select(tag => new ProductTag
                {
                    Tag = tag
                }).ToList() ?? new()
            }).ToList();

        await _seedRepository.AddProductsAsync(products, cancellationToken);
        return new SeedResult(categories.Count, products.Count);
    }
}
