using AutoMapper;
using ECommerce.Context;
using ECommerce.Dtos;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(AppDbContext context, IMapper mapper) : ControllerBase
{
	private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

	[Authorize(Roles = "User")]
	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
	{
		int userId = GetUserId();

		var cart = await context.Carts
			.Include(c => c.Items!)
			.ThenInclude(i => i.Product)
			.FirstOrDefaultAsync(c => c.UserId == userId);

		if (cart == null || !cart.Items!.Any())
			return BadRequest(new { message = "Cart is empty" });

		var order = new Order
		{
			UserId = userId,
			Status = "Pending",
			CreatedAt = DateTime.UtcNow,
			Items = cart.Items!.Select(i => new OrderItem
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				Price = i.Product!.Price
			}).ToList()
		};

		order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);

		await context.Orders.AddAsync(order);

		context.CartItems.RemoveRange(cart.Items!);

		await context.SaveChangesAsync();

		return Ok(mapper.Map<OrderOutputDto>(order));
	}

	[Authorize(Roles = "User")]
	[HttpGet]
	public async Task<IActionResult> GetMyOrders()
	{
		int userId = GetUserId();

		var orders = await context.Orders
			.Where(o => o.UserId == userId)
			.Include(o => o.Items!)
			.ThenInclude(i => i.Product)
			.ToListAsync();

		return Ok(mapper.Map<IEnumerable<OrderOutputDto>>(orders));
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetOrderById(int id)
	{
		int userId = GetUserId();

		var order = await context.Orders
			.Include(o => o.Items!)
			.ThenInclude(i => i.Product)
			.FirstOrDefaultAsync(o => o.Id == id);

		if (order == null)
			return NotFound(new { message = "Order not found" });

		if (order.UserId != userId && !User.IsInRole("Admin"))
			return Unauthorized();

		return Ok(mapper.Map<OrderOutputDto>(order));
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("all")]
	public async Task<IActionResult> GetAllOrders()
	{
		var orders = await context.Orders
			.Include(o => o.Items!)
			.ThenInclude(i => i.Product)
			.ToListAsync();

		return Ok(mapper.Map<IEnumerable<OrderOutputDto>>(orders));
	}
}
