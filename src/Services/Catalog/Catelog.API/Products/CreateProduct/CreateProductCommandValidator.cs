namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .Length(2, 150)
            .WithMessage("Product name must be between 2 and 150 characters.");

        RuleFor(command => command.Category)
            .NotEmpty()
            .WithMessage("Product category is required.");

        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Product description is required.")
            .Length(2, 150)
            .WithMessage("Product description must be between 2 and 150 characters.");

        RuleFor(command => command.ImageFile)
            .NotNull()
            .WithMessage("Product image file is required.");

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}