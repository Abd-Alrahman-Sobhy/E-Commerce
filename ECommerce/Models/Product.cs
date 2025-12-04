using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Product
{
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Name { get; set; } = null!;

	[StringLength(255)]
	public string? Description { get; set; }

	[Required]
	[Range(0.01, double.MaxValue)]
	public decimal Price { get; set; }

	[Url]
	public string? ImageUrl { get; set; }

	public int CategoryId { get; set; }
	public Category? Category { get; set; }
}
