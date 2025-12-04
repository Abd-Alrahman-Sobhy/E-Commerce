using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class UpdateUserInfoDto
{
	[Required, MinLength(3)]
	public string Username { get; set; } = null!;

	[Required, EmailAddress]
	public string Email { get; set; } = null!;
}
