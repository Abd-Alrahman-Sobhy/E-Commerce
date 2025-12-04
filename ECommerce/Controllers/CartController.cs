using AutoMapper;
using ECommerce.Context;
using ECommerce.Dtos;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CartController(AppDbContext context, IMapper mapper) : ControllerBase
{
	private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

	[HttpGet]
	public async Task<IActionResult> GetCart()
	{
		int userId = GetUserId();

		var cart = await context.Carts
			.Include(c => c.Items!)
			.ThenInclude(i => i.Product)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
			return Ok(new CartOutputDto { UserId = userId, Items = new List<CartItemOutputDto>(), TotalPrice = 0 });

		return Ok(mapper.Map<CartOutputDto>(cart));
	}

	[HttpPost("add")]
	public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
	{
		int userId = GetUserId();

		var product = await context.Products.FindAsync(dto.ProductId);
		if (product == null)
			return NotFound(new { message = "Product not found" });

		var cart = await context.Carts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
		{
			cart = new Cart { UserId = userId };
			await context.Carts.AddAsync(cart);
			await context.SaveChangesAsync();
		}

		var existingItem = cart.Items!.FirstOrDefault(x => x.ProductId == dto.ProductId);
		if (existingItem != null)
		{
			existingItem.Quantity += dto.Quantity;
		}
		else
		{
			cart.Items!.Add(new CartItem
			{
				ProductId = dto.ProductId,
				Quantity = dto.Quantity
			});
		}

		await context.SaveChangesAsync();
		return Ok(new { message = "Item added to cart" });
	}

	[HttpPut("update/{productId}")]
	public async Task<IActionResult> UpdateQuantity(int productId, [FromBody] CartItemUpdateDto dto)
	{
		int userId = GetUserId();

		var cart = await context.Carts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
			return NotFound(new { message = "Cart is empty" });

		var item = cart.Items!.FirstOrDefault(x => x.ProductId == productId);
		if (item == null)
			return NotFound(new { message = "Item not found" });

		item.Quantity = dto.Quantity;
		await context.SaveChangesAsync();

		return Ok(new { message = "Quantity updated" });
	}

	[HttpDelete("remove/{productId}")]
	public async Task<IActionResult> RemoveItem(int productId)
	{
		int userId = GetUserId();

		var cart = await context.Carts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null)
			return NotFound(new { message = "Cart is empty" });

		var item = cart.Items!.FirstOrDefault(x => x.ProductId == productId);
		if (item == null)
			return NotFound(new { message = "Item not found" });

		context.CartItems.Remove(item);
		await context.SaveChangesAsync();

		return Ok(new { message = "Item removed" });
	}

	[HttpDelete("clear")]
	public async Task<IActionResult> ClearCart()
	{
		int userId = GetUserId();

		var cart = await context.Carts
			.Include(c => c.Items)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null || !cart.Items!.Any())
			return Ok(new { message = "Cart is already empty" });

		context.CartItems.RemoveRange(cart.Items!);
		await context.SaveChangesAsync();

		return Ok(new { message = "Cart cleared" });
	}
}
