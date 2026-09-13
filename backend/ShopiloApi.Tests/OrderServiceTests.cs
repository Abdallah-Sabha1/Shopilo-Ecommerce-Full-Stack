using ShopiloApi.Exceptions;
using ShopiloApi.Interfaces;
using ShopiloApi.Models;
using ShopiloApi.Services;
using Xunit;

namespace ShopiloApi.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task PlaceOrderAsync_CreatesOrderAndUpdatesStock()
    {
        var product = new Product
        {
            Id = 1,
            Title = "Laptop",
            Price = 750m,
            Stock = 5
        };
        var cart = new Cart
        {
            CartItems =
            [
                new CartItem { Id = 10, ProductId = 1, Product = product, Quantity = 2 }
            ]
        };
        var cartRepository = new FakeCartRepository(cart);
        var orderRepository = new FakeOrderRepository();
        var service = new OrderService(cartRepository, orderRepository);

        Order order = await service.PlaceOrderAsync(
            cart.Id,
            "SAVE10",
            CancellationToken.None);

        Assert.Equal(1500m, order.Subtotal);
        Assert.Equal(150m, order.DiscountAmount);
        Assert.Equal(0m, order.ShippingCost);
        Assert.Equal(1350m, order.Total);
        Assert.Equal(3, product.Stock);
        Assert.Empty(cart.CartItems);
        Assert.Same(order, orderRepository.AddedOrder);
        Assert.True(orderRepository.SaveWasCalled);
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenCartIsEmpty_ThrowsEmptyCartException()
    {
        var cart = new Cart();
        var service = new OrderService(
            new FakeCartRepository(cart),
            new FakeOrderRepository());

        await Assert.ThrowsAsync<EmptyCartException>(() => service.PlaceOrderAsync(
            cart.Id,
            null,
            CancellationToken.None));
    }

    [Fact]
    public async Task PlaceOrderAsync_WhenStockIsTooLow_ThrowsInsufficientStockException()
    {
        var product = new Product { Id = 1, Title = "Laptop", Price = 750m, Stock = 1 };
        var cart = new Cart
        {
            CartItems =
            [
                new CartItem { ProductId = 1, Product = product, Quantity = 2 }
            ]
        };
        var service = new OrderService(
            new FakeCartRepository(cart),
            new FakeOrderRepository());

        await Assert.ThrowsAsync<InsufficientStockException>(() => service.PlaceOrderAsync(
            cart.Id,
            null,
            CancellationToken.None));
    }

    [Fact]
    public async Task PlaceOrderAsync_RoundsMoneyToTwoDecimalPlaces()
    {
        var product = new Product { Id = 1, Title = "Mascara", Price = 9.99m, Stock = 2 };
        var cart = new Cart
        {
            CartItems =
            [
                new CartItem { ProductId = 1, Product = product, Quantity = 1 }
            ]
        };
        var service = new OrderService(
            new FakeCartRepository(cart),
            new FakeOrderRepository());

        Order order = await service.PlaceOrderAsync(
            cart.Id,
            "SAVE10",
            CancellationToken.None);

        Assert.Equal(1.00m, order.DiscountAmount);
        Assert.Equal(16.98m, order.Total);
    }

    private sealed class FakeCartRepository : ICartRepository
    {
        private readonly Cart _cart;

        public FakeCartRepository(Cart cart) => _cart = cart;

        public Task<Cart> CreateAsync(CancellationToken cancellationToken) => Task.FromResult(_cart);

        public Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken) =>
            Task.FromResult<Cart?>(cartId == _cart.Id ? _cart : null);

        public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void RemoveItem(CartItem cartItem) => _cart.CartItems.Remove(cartItem);

        public void Clear(Cart cart) => cart.CartItems.Clear();
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        public Order? AddedOrder { get; private set; }
        public bool SaveWasCalled { get; private set; }

        public Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            AddedOrder = order;
            return Task.CompletedTask;
        }

        public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken) =>
            Task.FromResult(AddedOrder?.Id == orderId ? AddedOrder : null);

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            SaveWasCalled = true;
            return Task.CompletedTask;
        }
    }
}
