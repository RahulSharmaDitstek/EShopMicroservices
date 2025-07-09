using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Catalog.API.Data.TableConfigurations;
public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        entity.Property(b => b.Name).IsRequired().HasMaxLength(500);
        entity.Property(u => u.Description).IsRequired().HasMaxLength(255);
        entity.Property(u => u.ImageFile).IsRequired().HasMaxLength(255);
        //entity.Property(u => u.Price).IsRequired().HasColumnType("decimal(18,2)");
    }
}

