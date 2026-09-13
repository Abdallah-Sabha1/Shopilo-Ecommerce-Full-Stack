using Microsoft.AspNetCore.Mvc;
using ShopiloApi.DTOs.Products;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts(
        [FromQuery] string? search,
        [FromQuery] int? categoryId,
        [FromQuery] string? category,
        CancellationToken cancellationToken)
    {
        List<Product> products = await _productService.GetAllProductsAsync(
            search,
            categoryId,
            category,
            cancellationToken);

        return Ok(products.Select(ToDto).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductById(
        int id,
        CancellationToken cancellationToken)
    {
        Product product = await _productService.GetProductByIdAsync(id, cancellationToken);
        return Ok(ToDto(product));
    }

    private static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            DiscountPercentage = product.DiscountPercentage,
            Rating = product.Rating,
            Stock = product.Stock,
            Brand = product.Brand,
            Thumbnail = product.Thumbnail,
            CategoryId = product.CategoryId,
            Category = product.Category?.Slug ?? string.Empty,
            Images = product.ProductImages.Select(image => image.Url).ToList(),
            Tags = product.ProductTags.Select(tag => tag.Tag).ToList()
        };
    }
}
