namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotNull().WithMessage("Id is required");
        RuleFor(x => x.Order.OrderName).NotNull().WithMessage("Order Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required");
<<<<<<< HEAD
        RuleFor(x => x.Order.OrderName).NotNull().WithMessage("Customer Id is required")
            .MaximumLength(5).WithMessage("OrderName should be not exceed 5 words");
=======
>>>>>>> c5bc8b4a6334653d1e8a8cf72b2406d021f9e706
    }
}