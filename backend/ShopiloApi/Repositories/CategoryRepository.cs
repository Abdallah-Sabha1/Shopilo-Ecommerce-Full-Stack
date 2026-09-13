using Microsoft.EntityFrameworkCore;
using ShopiloApi.Data;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }
}
