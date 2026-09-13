using System.ComponentModel.DataAnnotations;

namespace ShopiloApi.Models
{
    public class Product
    {
        //Id, Title, Description, Price, DiscountPercentage,
        // Rating, Stock, Brand, Thumbnail
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public double Rating { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        public string Brand { get; set; } =string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductTag> ProductTags { get; set; } = new List<ProductTag>();


    }
}
