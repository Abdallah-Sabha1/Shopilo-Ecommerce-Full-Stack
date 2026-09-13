namespace ShopiloApi.Interfaces;

public interface ISeedService
{
    Task<SeedResult> SeedAsync(CancellationToken cancellationToken);
}

public record SeedResult(int CategoriesCount, int ProductsCount);
