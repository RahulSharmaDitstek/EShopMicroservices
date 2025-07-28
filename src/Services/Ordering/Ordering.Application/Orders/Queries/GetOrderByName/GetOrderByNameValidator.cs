namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameValidator : AbstractValidator<GetOrderByNameQuery>
{
    public GetOrderByNameValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
    }
}
