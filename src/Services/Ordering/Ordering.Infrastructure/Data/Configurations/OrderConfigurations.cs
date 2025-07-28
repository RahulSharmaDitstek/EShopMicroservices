namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasConversion
               (orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

        entity.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Cascade).IsRequired();

        entity.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId).IsRequired();

        entity.ComplexProperty(
            o => o.OrderName, orderEntity =>
            {
                orderEntity.Property(n => n.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });

        entity.ComplexProperty(
            o => o.ShippingAddress, addressEntity =>
            {
                addressEntity.Property(sa => sa.FirstName).IsRequired().HasMaxLength(50);
                addressEntity.Property(sa => sa.LastName).IsRequired().HasMaxLength(50);
                addressEntity.Property(sa => sa.EmailAddress).HasMaxLength(255);
                addressEntity.Property(sa => sa.AddressLine).IsRequired().HasMaxLength(255);
                addressEntity.Property(sa => sa.Country).HasMaxLength(100);
                addressEntity.Property(sa => sa.State).HasMaxLength(50);
                addressEntity.Property(sa => sa.ZipCode).HasMaxLength(50);
                addressEntity.Property(sa => sa.Country).HasMaxLength(6);
            });
        entity.ComplexProperty(
           o => o.BillingAddress, addressEntity =>
           {
               addressEntity.Property(ba => ba.FirstName).IsRequired().HasMaxLength(50);
               addressEntity.Property(ba => ba.LastName).IsRequired().HasMaxLength(50);
               addressEntity.Property(ba => ba.EmailAddress).HasMaxLength(255);
               addressEntity.Property(ba => ba.AddressLine).IsRequired().HasMaxLength(255);
               addressEntity.Property(ba => ba.Country).HasMaxLength(100);
               addressEntity.Property(ba => ba.State).HasMaxLength(50);
               addressEntity.Property(ba => ba.ZipCode).HasMaxLength(50);
               addressEntity.Property(ba => ba.Country).HasMaxLength(6);
           });

        entity.ComplexProperty(
           o => o.Payment, paymentEntity =>
           {
               paymentEntity.Property(ba => ba.CardName).HasMaxLength(50);
               paymentEntity.Property(ba => ba.CardNumber).IsRequired().HasMaxLength(16);
               paymentEntity.Property(ba => ba.Expiration).HasMaxLength(10);
               paymentEntity.Property(ba => ba.CVV).HasMaxLength(3);
               paymentEntity.Property(ba => ba.PaymentMethod);

           });

        entity.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status));


        entity.Property(e => e.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");

    }
}
