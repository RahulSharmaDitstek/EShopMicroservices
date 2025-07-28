namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotNull().WithMessage("Order Id is required to remove the Order");
    }
}