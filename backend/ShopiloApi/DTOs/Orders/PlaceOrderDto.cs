namespace ShopiloApi.DTOs.Orders;

public class PlaceOrderDto
{
    public Guid CartId { get; set; }
    public string? CouponCode { get; set; }
}
