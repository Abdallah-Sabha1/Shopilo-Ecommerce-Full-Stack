using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface ICartService
{
    Task<Cart> CreateCartAsync(CancellationToken cancellationToken);
    Task<Cart> GetCartByIdAsync(Guid cartId, CancellationToken cancellationToken);
    Task<Cart> AddItemAsync(Guid cartId, int productId, int quantity, CancellationToken cancellationToken);
    Task<Cart> UpdateItemQuantityAsync(Guid cartId, int cartItemId, int quantity, CancellationToken cancellationToken);
    Task<Cart> RemoveItemAsync(Guid cartId, int cartItemId, CancellationToken cancellationToken);
    Task ClearCartAsync(Guid cartId, CancellationToken cancellationToken);
}
