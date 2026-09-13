namespace ShopiloApi.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Placed;
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}
