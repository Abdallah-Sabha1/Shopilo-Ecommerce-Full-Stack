namespace ShopiloApi.Models
{
    public class Cart
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
