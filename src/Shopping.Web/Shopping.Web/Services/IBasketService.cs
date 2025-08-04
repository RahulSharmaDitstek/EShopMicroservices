namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest userName);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest userName);

    public async Task<ShoppingCartModel> LoadUserBasket()
    {
        var userName = "swn";
        ShoppingCartModel basket;
        try
        {
            var getBasketResponse = await GetBasket(userName);
            basket = getBasketResponse.ShoppingCartModel;

        }
        catch (ApiException ApiException) when (ApiException.StatusCode == HttpStatusCode.NotFound)
        {
            basket = new ShoppingCartModel
            {
                UserName = userName,
                Items = []
            };
        }
        return basket;

    }
}
