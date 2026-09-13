using ShopiloApi.Models;

namespace ShopiloApi.DTOs.Cart
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public decimal LineTotal { get; set; }    // UnitPrice × Quantity
    }

}
