using Microsoft.EntityFrameworkCore;
using ShopiloApi.Data;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Repositories;

public class SeedRepository : ISeedRepository
{
    private readonly AppDbContext _dbContext;

    public SeedRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> HasDataAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Categories.AnyAsync(cancellationToken) ||
               await _dbContext.Products.AnyAsync(cancellationToken);
    }

    public async Task AddCategoriesAsync(
        List<Category> categories,
        CancellationToken cancellationToken)
    {
        await _dbContext.Categories.AddRangeAsync(categories, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddProductsAsync(
        List<Product> products,
        CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddRangeAsync(products, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
