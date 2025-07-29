namespace Ordering.Infrastructure.Data;

public class OrderingDbContext(DbContextOptions<OrderingDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ProductConfigurations());
        //modelBuilder.ApplyConfiguration(new CustomerConfigurations());
        //modelBuilder.ApplyConfiguration(new OrderConfigurations());
        //modelBuilder.ApplyConfiguration(new OrderItemConfigurations());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
