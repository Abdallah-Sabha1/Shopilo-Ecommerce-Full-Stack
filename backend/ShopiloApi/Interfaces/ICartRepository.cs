using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface ICartRepository
{
    Task<Cart> CreateAsync(CancellationToken cancellationToken);
    Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void RemoveItem(CartItem cartItem);
    void Clear(Cart cart);
}
