using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IGenericRepository<Category> repo, IMapper mapper) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var categories = await repo.GetAllAsync();
		var result = mapper.Map<IEnumerable<CategoryOutputDto>>(categories);
		return Ok(result);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var category = await repo.GetByIdAsync(id);

		return category == null ? NotFound(new { Message = "Category not found" }) : Ok(mapper.Map<CategoryOutputDto>(category));
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
	{
		var category = mapper.Map<Category>(dto);
		await repo.AddAsync(category);

		return Created();
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
	{
		var category = await repo.GetByIdAsync(id);

		if (category == null) return NotFound(new { Message = "Category not found" });

		mapper.Map(dto, category);
		await repo.UpdateAsync(category);

		return NoContent();
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var deleted = await repo.DeleteAsync(id);

		if (!deleted) return NotFound(new { Message = "Category not found" });

		return NoContent();
	}
}
