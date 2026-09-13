using Microsoft.AspNetCore.Mvc;
using ShopiloApi.Interfaces;

namespace ShopiloApi.Controllers;

[Route("api/seed")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly ISeedService _seedService;
    private readonly IWebHostEnvironment _environment;

    public SeedController(ISeedService seedService, IWebHostEnvironment environment)
    {
        _seedService = seedService;
        _environment = environment;
    }

    [HttpPost]
    public async Task<IActionResult> SeedData(CancellationToken cancellationToken)
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }

        SeedResult result = await _seedService.SeedAsync(cancellationToken);
        return Ok(new
        {
            message = "Sample data imported successfully.",
            categoriesCount = result.CategoriesCount,
            productsCount = result.ProductsCount
        });
    }
}
