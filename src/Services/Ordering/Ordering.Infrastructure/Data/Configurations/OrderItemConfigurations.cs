namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> entity)
    {
        entity.HasKey(oi => oi.Id);
        entity.Property(oi => oi.Id)
            .HasConversion(
                orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));

        entity.HasOne<Product>()
              .WithMany()
              .HasForeignKey(oi => oi.ProductId);

        entity.Property(oi => oi.Quantity).IsRequired().HasMaxLength(1);

        entity.Property(oi => oi.Price).IsRequired().HasColumnType("decimal(18,2)");

    }
}
