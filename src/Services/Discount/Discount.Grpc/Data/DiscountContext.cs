namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DiscountConfig());

    }
}
