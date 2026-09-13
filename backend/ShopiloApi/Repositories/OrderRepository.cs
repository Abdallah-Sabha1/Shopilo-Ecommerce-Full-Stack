using Microsoft.EntityFrameworkCore;
using ShopiloApi.Data;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;

namespace ShopiloApi.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public Task AddAsync(Order order, CancellationToken cancellationToken) =>
        _dbContext.Orders.AddAsync(order, cancellationToken).AsTask();

    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
