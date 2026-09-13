using ShopiloApi.Exceptions;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public Task<Cart> CreateCartAsync(CancellationToken cancellationToken) =>
        _cartRepository.CreateAsync(cancellationToken);

    public async Task<Cart> GetCartByIdAsync(Guid cartId, CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken);
        return cart ?? throw new CartNotFoundException(cartId);
    }

    public async Task<Cart> AddItemAsync(
        Guid cartId,
        int productId,
        int quantity,
        CancellationToken cancellationToken)
    {
        ValidateQuantity(quantity);

        Cart cart = await GetCartByIdAsync(cartId, cancellationToken);
        Product? product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(productId);
        }

        CartItem? existingItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
        int requestedQuantity = (existingItem?.Quantity ?? 0) + quantity;

        if (requestedQuantity > product.Stock)
        {
            throw new InsufficientStockException(productId, requestedQuantity, product.Stock);
        }

        if (existingItem is null)
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            });
        }
        else
        {
            existingItem.Quantity = requestedQuantity;
        }

        await _cartRepository.SaveChangesAsync(cancellationToken);
        return await GetCartByIdAsync(cartId, cancellationToken);
    }

    public async Task<Cart> UpdateItemQuantityAsync(
        Guid cartId,
        int cartItemId,
        int quantity,
        CancellationToken cancellationToken)
    {
        ValidateQuantity(quantity);

        Cart cart = await GetCartByIdAsync(cartId, cancellationToken);
        CartItem? cartItem = cart.CartItems.FirstOrDefault(item => item.Id == cartItemId);
        if (cartItem is null)
        {
            throw new CartItemNotFoundException(cartItemId);
        }

        if (cartItem.Product is null)
        {
            throw new ProductNotFoundException(cartItem.ProductId);
        }

        if (quantity > cartItem.Product.Stock)
        {
            throw new InsufficientStockException(cartItem.ProductId, quantity, cartItem.Product.Stock);
        }

        cartItem.Quantity = quantity;
        await _cartRepository.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<Cart> RemoveItemAsync(
        Guid cartId,
        int cartItemId,
        CancellationToken cancellationToken)
    {
        Cart cart = await GetCartByIdAsync(cartId, cancellationToken);
        CartItem? cartItem = cart.CartItems.FirstOrDefault(item => item.Id == cartItemId);
        if (cartItem is null)
        {
            throw new CartItemNotFoundException(cartItemId);
        }

        _cartRepository.RemoveItem(cartItem);
        await _cartRepository.SaveChangesAsync(cancellationToken);
        cart.CartItems.Remove(cartItem);
        return cart;
    }

    public async Task ClearCartAsync(Guid cartId, CancellationToken cancellationToken)
    {
        Cart cart = await GetCartByIdAsync(cartId, cancellationToken);
        _cartRepository.Clear(cart);
        await _cartRepository.SaveChangesAsync(cancellationToken);
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
    }
}
