using Microsoft.EntityFrameworkCore;
using ShopiloApi.Data;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _dbContext;

    public CartRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<Cart> CreateAsync(CancellationToken cancellationToken)
    {
        var cart = new Cart();
        await _dbContext.Carts.AddAsync(cart, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken)
    {
        return _dbContext.Carts
            .Include(cart => cart.CartItems)
            .ThenInclude(cartItem => cartItem.Product)
            .FirstOrDefaultAsync(cart => cart.Id == cartId, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public void RemoveItem(CartItem cartItem) => _dbContext.CartItems.Remove(cartItem);

    public void Clear(Cart cart) => _dbContext.CartItems.RemoveRange(cart.CartItems);
}
