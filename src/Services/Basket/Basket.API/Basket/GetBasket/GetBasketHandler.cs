namespace Basket.API.Basket.GetBasket;

public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var basket = await repository.GetBasket(request.UserName);
            return new GetBasketResult(basket);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
