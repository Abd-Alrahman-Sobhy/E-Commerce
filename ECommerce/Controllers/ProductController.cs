using AutoMapper;
using ECommerce.Context;
using ECommerce.Dtos;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IGenericRepository<Product> repo, AppDbContext context, IMapper mapper) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var products = await context.Products
			.Include(p => p.Category)
			.ToListAsync();

		var result = mapper.Map<IEnumerable<ProductOutputDto>>(products);
		return Ok(result);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var product = await context.Products
			.Include(p => p.Category)
			.FirstOrDefaultAsync(p => p.Id == id);

		if (product == null)
			return NotFound(new { Message = "Product not found" });

		return Ok(mapper.Map<ProductOutputDto>(product));
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
	{
		var exists = await context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
		if (!exists)
			return BadRequest(new { Message = "Category does not exist" });

		var product = mapper.Map<Product>(dto);

		await repo.AddAsync(product);

		return Created();
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
	{
		var product = await context.Products.FindAsync(id);
		if (product == null)
			return NotFound(new { Message = "Product not found" });

		var categoryExists = await context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
		if (!categoryExists)
			return BadRequest(new { Message = "Category does not exist" });

		mapper.Map(dto, product);
		await repo.UpdateAsync(product);

		return NoContent();
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var product = await repo.DeleteAsync(id);
		if (!product)
			return NotFound(new { Message = "Product not found" });

		return NoContent();
	}
}
