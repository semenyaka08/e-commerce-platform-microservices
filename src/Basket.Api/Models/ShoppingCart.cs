namespace Basket.Api.Models;

public class ShoppingCart(string userName)
{
    public string UserName { get; set; } = userName;

    public List<ShoppingCartItem> CartItems { get; set; } = [];

    public decimal TotalPrice => CartItems.Sum(x=>x.Price * x.Quantity);
}