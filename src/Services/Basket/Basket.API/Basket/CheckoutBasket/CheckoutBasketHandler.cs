namespace Basket.API.Basket.CheckoutBasket;
public class CheckoutBasketHandler(ILogger<CheckoutBasketHandler> logger, IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResponse>
{
    public async Task<CheckoutBasketResponse> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Processing checkout for basket with Name: {UserName}", command.BasketCheckoutDto.UserName);

            var basket = await basketRepository.GetBasket(command.BasketCheckoutDto.UserName);

            if (basket == null)
            {
                logger.LogWarning("Basket not found for UserName: {UserName}", command.BasketCheckoutDto.UserName);
                return new CheckoutBasketResponse(false);
            }

            var eventMessage =command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            logger.LogInformation("Basket checkout event published for UserName: {UserName}", command.BasketCheckoutDto.UserName);

            // Clear the basket after checkout
            await basketRepository.DeleteBasket(command.BasketCheckoutDto.UserName);

            logger.LogInformation("Basket cleared for UserName: {UserName}", command.BasketCheckoutDto.UserName);

            return new CheckoutBasketResponse(true);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
