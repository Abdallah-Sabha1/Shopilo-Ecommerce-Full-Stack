
namespace ShopiloApi.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
    }

    public class ProductReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
    }
}
