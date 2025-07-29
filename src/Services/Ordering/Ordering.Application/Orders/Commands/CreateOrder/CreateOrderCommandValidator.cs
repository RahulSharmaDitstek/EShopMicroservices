namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotNull().WithMessage("Order Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required");
        RuleFor(x => x.Order.OrderItems).NotNull().WithMessage("Order should not be empty");
    }
}