using ShopiloApi.Models;

namespace ShopiloApi.DTOs.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }

        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        public decimal Subtotal { get; set; }
        public int ItemCount { get; set; }
    }
}
