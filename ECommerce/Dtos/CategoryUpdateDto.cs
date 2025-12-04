using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class CategoryUpdateDto
{
	[StringLength(100)]
	public string? Name { get; set; }

	[StringLength(255)]
	public string? Description { get; set; }
}
