namespace ECommerce.Dtos;

public class OrderOutputDto
{
	public int Id { get; set; }
	public decimal TotalAmount { get; set; }
	public string Status { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public List<OrderItemOutputDto> Items { get; set; } = new();
}

