namespace Basket.API.Basket.StoreBasket;
public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    private readonly IBasketRepository _repository = repository;

    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        if (command.Cart is null)
        {
            Results.BadRequest(new { Field = nameof(command.Cart), Message = "Shopping cart cannot be null." });
        }
        await DeductDiscount(command!.Cart!, cancellationToken);

        await _repository.StoreBasket(command.Cart!, cancellationToken);

        return new StoreBasketResult(command.Cart!.UserName);
    }
    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart!.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            if (coupon is not null && coupon.Amount > 0)
            {
                item.Price -= coupon.Amount;
            }

        }
    }
}
