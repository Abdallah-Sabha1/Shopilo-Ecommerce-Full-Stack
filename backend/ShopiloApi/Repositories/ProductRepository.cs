using Microsoft.EntityFrameworkCore;
using ShopiloApi.Data;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public Task<List<Product>> GetAllAsync(
        string? search,
        int? categoryId,
        string? categorySlug,
        CancellationToken cancellationToken)
    {
        IQueryable<Product> query = _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .Include(product => product.ProductImages)
            .Include(product => product.ProductTags);

        if (!string.IsNullOrWhiteSpace(search))
        {
            string searchTerm = search.Trim();
            query = query.Where(product =>
                product.Title.Contains(searchTerm) ||
                product.Description.Contains(searchTerm) ||
                product.Brand.Contains(searchTerm) ||
                product.ProductTags.Any(tag => tag.Tag.Contains(searchTerm)));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(categorySlug))
        {
            query = query.Where(product => product.Category.Slug == categorySlug);
        }

        return query.OrderBy(product => product.Id).ToListAsync(cancellationToken);
    }

    public Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken)
    {
        return _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .Include(product => product.ProductImages)
            .Include(product => product.ProductTags)
            .FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }
}
