namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
