namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Id).HasConversion(
                customerId => customerId.Value,
                dbId => CustomerId.Of(dbId));

        entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

        entity.Property(c => c.Email).IsRequired().HasMaxLength(255);
        entity.HasIndex(c => c.Email).IsUnique();   
    }
}
