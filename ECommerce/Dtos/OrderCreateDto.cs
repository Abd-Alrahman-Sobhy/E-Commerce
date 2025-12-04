using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class OrderCreateDto
{
	[Required, MinLength(5)]
	public string ShippingAddress { get; set; } = string.Empty;

	[Required, MinLength(2)]
	public string City { get; set; } = string.Empty;

	[Required, MinLength(2)]
	public string Country { get; set; } = string.Empty;
}
