namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    public ShoppingCart(string userName)
    {
        UserName = userName ?? string.Empty;
    }
    //Required for Mapping
    public ShoppingCart()
    {

    }
}
