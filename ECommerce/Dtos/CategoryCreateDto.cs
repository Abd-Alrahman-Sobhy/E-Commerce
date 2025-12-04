using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class CategoryCreateDto
{
	[Required]
	[StringLength(100)]
	public string Name { get; set; } = null!;

	[StringLength(255)]
	public string? Description { get; set; }
}
