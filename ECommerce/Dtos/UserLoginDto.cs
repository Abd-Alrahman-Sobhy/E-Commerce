using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class UserLoginDto
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	[StringLength(100, MinimumLength = 6)]
	public string Password { get; set; } = null!;
}
