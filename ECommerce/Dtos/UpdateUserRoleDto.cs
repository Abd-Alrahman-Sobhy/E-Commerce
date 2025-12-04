using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos;

public class UpdateUserRoleDto
{
	[Required]
	[RegularExpression("Admin|User", ErrorMessage = "Role must be either 'Adming' or 'User'")]
	public string Role { get; set; } = null!;
}
