using ECommerce.Context;
using ECommerce.Dtos;
using ECommerce.Models;
using ECommerce.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AppDbContext context, JwtService jwtService) : ControllerBase
{
	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
	{
		if (await context.Users.AnyAsync(u => u.Email == dto.Email))
			return BadRequest(new { Message = "Email already exists" });

		var user = new User
		{
			Username = dto.Username,
			Email = dto.Email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
			Role = "User",
			RefreshToken = jwtService.GenerateRefreshToken(),
			RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(HttpContext.RequestServices.GetRequiredService<IConfiguration>()["JwtSettings:RefreshTokenExpirationDays"]!))
		};

		context.Users.Add(user);
		await context.SaveChangesAsync();

		return Created();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
		if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
			return BadRequest(new { message = "Invalid credentials" });

		user.RefreshToken = jwtService.GenerateRefreshToken();
		user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(HttpContext.RequestServices.GetRequiredService<IConfiguration>()["JwtSettings:RefreshTokenExpirationDays"]!));
		await context.SaveChangesAsync();

		var accessToken = jwtService.GenerateAccessToken(user);

		return Ok(new
		{
			AccessToken = accessToken,
			RefreshToken = user.RefreshToken
		});
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
		if (user == null)
			return BadRequest(new { Message = "Invalid or expired refresh token" });

		user.RefreshToken = jwtService.GenerateRefreshToken();
		user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(HttpContext.RequestServices.GetRequiredService<IConfiguration>()["JwtSettings:RefreshTokenExpirationDays"]!));
		await context.SaveChangesAsync();

		var accessToken = jwtService.GenerateAccessToken(user);

		return Ok(new
		{
			AccessToken = accessToken,
			RefreshToken = user.RefreshToken
		});
	}

	[HttpGet("profile")]
	[Authorize]
	public async Task<IActionResult> Profile()
	{
		var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
		var user = await context.Users.FindAsync(userId);

		if (user == null)
			return NotFound();

		return Ok(new { user.Id, user.Username, user.Email, user.Role });
	}
}
