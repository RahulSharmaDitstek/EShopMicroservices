namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItemModel> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

}
