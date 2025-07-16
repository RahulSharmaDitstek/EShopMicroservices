namespace Discount.Grpc.Data;

public class DiscountConfig : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(b => b.ProductName).IsRequired().HasMaxLength(100);
        entity.Property(b => b.Description).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
        entity.HasData(GetPreconfiguredCoupons());
    }
    private static IEnumerable<Coupon> GetPreconfiguredCoupons() =>
    [
        new Coupon
    {
        Id = 1,
        ProductName = "Apple iPhone 15",
        Description = "Discount on iPhone 15 purchase.",
        Amount = 150.00m
    },
    new Coupon
    {
        Id = 2,
        ProductName = "Samsung Galaxy S24",
        Description = "Save on Samsung Galaxy S24.",
        Amount = 120.00m
    },
    new Coupon
    {
        Id = 3,
        ProductName = "Sony WH-1000XM5",
        Description = "Noise-canceling headphones discount.",
        Amount = 50.00m
    },
    new Coupon
    {
        Id = 4,
        ProductName = "Dell XPS 15",
        Description = "Exclusive Dell XPS 15 discount.",
        Amount = 200.00m
    },
    new Coupon
    {
        Id = 5,
        ProductName = "Apple MacBook Air M3",
        Description = "Special discount on MacBook Air M3.",
        Amount = 180.00m
    },
    new Coupon
    {
        Id = 6,
        ProductName = "Logitech MX Master 3S",
        Description = "Save on premium Logitech mouse.",
        Amount = 20.00m
    },
    new Coupon
    {
        Id = 7,
        ProductName = "Asus ROG Strix RTX 4080",
        Description = "Graphics card discount offer.",
        Amount = 250.00m
    },
    new Coupon
    {
        Id = 8,
        ProductName = "Canon EOS R7",
        Description = "Camera purchase discount.",
        Amount = 175.00m
    },
    new Coupon
    {
        Id = 9,
        ProductName = "Amazon Echo Dot (5th Gen)",
        Description = "Discount on smart speaker.",
        Amount = 15.00m
    },
    new Coupon
    {
        Id = 10,
        ProductName = "Fitbit Charge 6",
        Description = "Fitness tracker discount.",
        Amount = 30.00m
    }
    ];

}