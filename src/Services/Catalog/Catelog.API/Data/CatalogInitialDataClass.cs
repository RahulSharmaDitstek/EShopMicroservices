using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data;

public class CatalogInitialDataClass(DbContextOptions<CatalogInitialDataClass> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfig());
    }
}
