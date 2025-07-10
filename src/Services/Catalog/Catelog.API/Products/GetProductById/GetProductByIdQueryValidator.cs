namespace Catalog.API.Products.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(query => query.Id)
                   .NotEmpty()
                   .WithMessage("Product ID is required.");
    }
}