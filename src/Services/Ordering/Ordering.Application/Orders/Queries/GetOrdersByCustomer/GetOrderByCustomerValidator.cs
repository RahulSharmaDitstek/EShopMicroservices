namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrderByCustomerValidator : AbstractValidator<GetOrderByCustomerQuery>
{
    public GetOrderByCustomerValidator()
    {
        RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer Id is required");
    }
}
