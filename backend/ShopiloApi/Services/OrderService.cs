using ShopiloApi.Exceptions;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Services;

public class OrderService : IOrderService
{
    private static readonly Dictionary<string, int> CouponDiscounts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["SHOPLIO20"] = 20,
        ["SAVE10"] = 10,
        ["FIRST15"] = 15
    };

    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Order> PlaceOrderAsync(
        Guid cartId,
        string? couponCode,
        CancellationToken cancellationToken)
    {
        Cart? cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken);
        if (cart is null)
        {
            throw new CartNotFoundException(cartId);
        }

        if (cart.CartItems.Count == 0)
        {
            throw new EmptyCartException();
        }

        foreach (CartItem cartItem in cart.CartItems)
        {
            if (cartItem.Product is null)
            {
                throw new ProductNotFoundException(cartItem.ProductId);
            }

            if (cartItem.Quantity > cartItem.Product.Stock)
            {
                throw new InsufficientStockException(
                    cartItem.ProductId,
                    cartItem.Quantity,
                    cartItem.Product.Stock);
            }
        }

        decimal subtotal = RoundCurrency(
            cart.CartItems.Sum(item => item.Product!.Price * item.Quantity));
        string? normalizedCoupon = string.IsNullOrWhiteSpace(couponCode)
            ? null
            : couponCode.Trim().ToUpperInvariant();
        int discountPercentage = GetDiscountPercentage(normalizedCoupon);
        decimal discountAmount = RoundCurrency(subtotal * discountPercentage / 100m);
        decimal shippingCost = subtotal > 50m ? 0m : 7.99m;

        var order = new Order
        {
            OrderNumber = $"SHP-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}",
            Subtotal = subtotal,
            DiscountAmount = discountAmount,
            ShippingCost = shippingCost,
            Total = RoundCurrency(subtotal - discountAmount + shippingCost),
            CouponCode = normalizedCoupon,
            Items = cart.CartItems.Select(cartItem => new OrderItem
            {
                ProductId = cartItem.ProductId,
                ProductTitle = cartItem.Product!.Title,
                UnitPrice = cartItem.Product.Price,
                Quantity = cartItem.Quantity,
                LineTotal = cartItem.Product.Price * cartItem.Quantity
            }).ToList()
        };

        foreach (CartItem cartItem in cart.CartItems)
        {
            cartItem.Product!.Stock -= cartItem.Quantity;
        }

        await _orderRepository.AddAsync(order, cancellationToken);
        _cartRepository.Clear(cart);
        await _orderRepository.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        return order ?? throw new OrderNotFoundException(orderId);
    }

    private static int GetDiscountPercentage(string? couponCode)
    {
        if (couponCode is null)
        {
            return 0;
        }

        if (!CouponDiscounts.TryGetValue(couponCode, out int discountPercentage))
        {
            throw new ArgumentException("The coupon code is invalid.");
        }

        return discountPercentage;
    }

    private static decimal RoundCurrency(decimal amount) =>
        decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
}
