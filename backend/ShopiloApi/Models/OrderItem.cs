namespace ShopiloApi.Models;

public class OrderItem
{
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
    public Order Order { get; set; } = null!;
}
