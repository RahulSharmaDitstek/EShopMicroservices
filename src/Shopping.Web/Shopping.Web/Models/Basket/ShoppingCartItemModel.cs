namespace Shopping.Web.Models.Basket;

public class ShoppingCartItemModel
{
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
}

//wrapper classes

public record GetBasketResponse(ShoppingCartModel ShoppingCartModel);
public record StoreBasketRequest(ShoppingCartModel ShoppingCartModel);
public record StoreBasketResponse(string UserName);
public record DeleteBasketResponse(bool IsSuccess);

