using ECommerce.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Services.Implementation;

public class JwtService(IConfiguration configuration)
{
	public string GenerateAccessToken(User user)
	{
		var secretKey = configuration["JwtSettings:SecretKey"];
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role)
		};

		var token = new JwtSecurityToken(
				issuer: configuration["JwtSettings:Issuer"],
				audience: configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(double.Parse(configuration["JwtSettings:AccessTokenExpirationMinutes"]!)),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}
}
