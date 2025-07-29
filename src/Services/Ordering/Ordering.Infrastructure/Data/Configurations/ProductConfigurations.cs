namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Id).HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId));

        entity.Property(p => p.Name).IsRequired().HasMaxLength(100);

        //entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
    }
}
