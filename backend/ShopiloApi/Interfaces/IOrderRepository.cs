using ShopiloApi.Models;

namespace ShopiloApi.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
