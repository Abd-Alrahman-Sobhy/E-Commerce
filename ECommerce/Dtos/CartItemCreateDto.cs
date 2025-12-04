using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class CartItemUpdateDto
{
	[Required]
	[Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
	public int Quantity { get; set; }
}
