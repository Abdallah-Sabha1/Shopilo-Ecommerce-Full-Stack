using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(Guid cartId, string? couponCode, CancellationToken cancellationToken);
    Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
}
