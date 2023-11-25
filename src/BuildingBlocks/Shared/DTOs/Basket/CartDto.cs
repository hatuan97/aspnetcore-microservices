namespace Shared.DTOs.Basket;

public class CartDto
{
    public CartDto()
    {
    }

    public CartDto(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public string EmailAddress { get; set; }

    public List<CartItemDto> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
}