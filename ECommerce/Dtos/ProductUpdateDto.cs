using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class ProductUpdateDto
{
	[StringLength(100)]
	public string? Name { get; set; }

	[StringLength(255)]
	public string? Description { get; set; }

	[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
	public decimal? Price { get; set; }

	[Url]
	public string? ImageUrl { get; set; }

	public int? CategoryId { get; set; }
}
