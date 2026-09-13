using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository) =>
        _categoryRepository = categoryRepository;

    public Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken) =>
        _categoryRepository.GetAllAsync(cancellationToken);
}
