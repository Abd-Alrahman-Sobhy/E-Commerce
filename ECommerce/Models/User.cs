using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class User
{
	public int Id { get; set; }
	[Required]
	[StringLength(50, MinimumLength = 3)]
	public string Username { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	[StringLength(255, MinimumLength = 6)]
	public string PasswordHash { get; set; } = null!;

	[Required]
	[RegularExpression("User|Admin")]
	public string Role { get; set; } = "User";

	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpiry { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
