using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Category>().HasData(
		new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
		new Category { Id = 2, Name = "Clothes", Description = "Men and Women clothes" },
		new Category { Id = 3, Name = "Books", Description = "Educational and entertainment books" });

		modelBuilder.Entity<Product>().HasData(
			new Product
			{
				Id = 1,
				Name = "Laptop",
				Description = "High performance laptop",
				Price = 15000,
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSbzD10aeIlMxLlgwwq7aj7LxlwWQfT0lG6kQ&s",
				CategoryId = 1
			},
			new Product
			{
				Id = 2,
				Name = "Smartphone",
				Description = "Latest model smartphone",
				Price = 10000,
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQTbSJ0HUqvD5g9eSAbjn3HQp3fcoHsU-P3zw&s",
				CategoryId = 1
			},
			new Product
			{
				Id = 3,
				Name = "T-Shirt",
				Description = "Cotton t-shirt",
				Price = 250,
				ImageUrl = "https://colorfulstandard.com/cdn/shop/files/CS2056_Male_OversizedOrganicT-Shirt-DeepBlack_2_2adc696d-0930-4a7f-86b1-61ad0c6dc3e9.jpg?v=1745834597&width=600",
				CategoryId = 2
			},
			new Product
			{
				Id = 4,
				Name = "C# Programming Book",
				Description = "Learn C# from basics to advanced",
				Price = 400,
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdikZIK-hOY-idjrYVnCck0STgN0zZre9DWg&s",
				CategoryId = 3
			});
	}
}
