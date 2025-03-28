namespace Basket.Api.Models;

public class ShoppingCartItem
{
    public string ProductName { get; set; } = string.Empty;
    
    public int Quantity { get; set; }

    public Guid ProductId { get; set; }

    public decimal Price { get; set; }
}