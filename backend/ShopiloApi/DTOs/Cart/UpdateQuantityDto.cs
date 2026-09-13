using System.ComponentModel.DataAnnotations;

namespace ShopiloApi.DTOs.Cart;

public class UpdateQuantityDto
{
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
