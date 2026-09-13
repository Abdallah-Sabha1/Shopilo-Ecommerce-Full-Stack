namespace ShopiloApi.DTOs.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
