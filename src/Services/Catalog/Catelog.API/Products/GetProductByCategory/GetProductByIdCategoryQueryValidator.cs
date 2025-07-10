namespace Catalog.API.Products.GetProductByCategory;

public class GetProductByIdCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
{
    public GetProductByIdCategoryQueryValidator()
    {
        RuleFor(query => query.Category)
                   .NotEmpty()
                   .WithMessage("Category Name is required.");
    }
}