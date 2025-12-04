namespace ECommerce.Dtos;

public class CartOutputDto
{
	public int UserId { get; set; }
	public List<CartItemOutputDto> Items { get; set; } = new();
	public decimal TotalPrice { get; set; }
}
