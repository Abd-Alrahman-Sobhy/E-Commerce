using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class ProductCreateDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; } = null!;

	[StringLength(255)]
	public string? Description { get; set; }

	[Required]
	[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
	public decimal Price { get; set; }

	[Url]
	public string? ImageUrl { get; set; }

	[Required]
	public int CategoryId { get; set; }
}
