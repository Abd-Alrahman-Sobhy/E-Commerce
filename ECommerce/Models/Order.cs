using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Order
{
	public int Id { get; set; }
	public int UserId { get; set; }

	[Required]
	[Range(0.01, double.MaxValue)]
	public decimal TotalAmount { get; set; }

	[Required]
	[StringLength(50)]
	public string Status { get; set; } = "Pending";
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public ICollection<OrderItem>? Items { get; set; }
}
