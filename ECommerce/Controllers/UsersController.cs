using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IGenericRepository<User> repo, IMapper mapper) : ControllerBase
{
	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> GetUsers()
	{
		var users = await repo.GetAllAsync();
		var dto = mapper.Map<IEnumerable<UserOutputDto>>(users);
		return Ok(dto);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetUser(int id)
	{
		var user = await repo.GetByIdAsync(id);
		if (user == null)
			return NotFound(new { Message = "User not found" });

		return Ok(mapper.Map<UserOutputDto>(user));
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("{id:int}/role")]
	public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleDto dto)
	{
		var user = await repo.GetByIdAsync(id);
		if (user == null)
			return NotFound(new { Message = "User not found" });

		user.Role = dto.Role;

		await repo.UpdateAsync(user);

		return NoContent();
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserInfoDto dto)
	{
		var user = await repo.GetByIdAsync(id);
		if (user == null)
			return NotFound(new { Message = "User not found" });

		mapper.Map(dto, user);
		await repo.UpdateAsync(user);

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var isDeleted = await repo.DeleteAsync(id);
		if (!isDeleted)
			return NotFound(new { Message = "User not found" });

		return NoContent();
	}
}
