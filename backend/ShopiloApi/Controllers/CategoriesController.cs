using Microsoft.AspNetCore.Mvc;
using ShopiloApi.DTOs.Categories;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) =>
        _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories(
        CancellationToken cancellationToken)
    {
        List<Category> categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        return Ok(categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Url = $"https://dummyjson.com/products/category/{category.Slug}"
        }).ToList());
    }
}
