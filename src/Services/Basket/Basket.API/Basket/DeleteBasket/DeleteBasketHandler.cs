
namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    private readonly IBasketRepository _repository = repository;

    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteBasket(command.UserName, cancellationToken);

        //TODO delete basket drom db and cache
        //session.Delete<Product>(command.Id);
        return new DeleteBasketResult(true);
    }
}
