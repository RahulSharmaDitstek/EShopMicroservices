using Marten.Schema;
namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellationToken)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(token: cancellationToken))
            return;

        //MARTEN UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellationToken);
    }
    private static IEnumerable<Product> GetPreconfiguredProducts() =>
        [
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Apple iPhone 15",
            Category = ["Electronics", "Mobile"],
            Description = "Latest Apple iPhone with A17 chip and dual camera system.",
            ImageFile = "iphone15.jpg",
            Price = 999.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Samsung Galaxy S24",
            Category =  ["Electronics", "Mobile"],
            Description = "High-end Android phone with amazing display and performance.",
            ImageFile = "galaxyS24.jpg",
            Price = 899.50m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Sony WH-1000XM5",
            Category = ["Electronics", "Audio"],
            Description = "Industry-leading noise-canceling over-ear headphones.",
            ImageFile = "sony-headphones.jpg",
            Price = 349.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Dell XPS 15",
            Category = ["Computers", "Laptops"],
            Description = "Premium laptop with 4K display and powerful specs.",
            ImageFile = "dell-xps15.jpg",
            Price = 1799.00m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Apple MacBook Air M3",
            Category = [ "Computers", "Laptops" ],
            Description = "Lightweight and powerful laptop with M3 chip.",
            ImageFile = "macbook-air.jpg",
            Price = 1199.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Logitech MX Master 3S",
            Category = ["Accessories", "Mouse"],
            Description = "High-precision wireless mouse for productivity.",
            ImageFile = "logitech-mx.jpg",
            Price = 99.95m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Asus ROG Strix RTX 4080",
            Category =  ["Computers", "Graphics Cards"],
            Description = "Next-gen NVIDIA graphics card for gaming and rendering.",
            ImageFile = "rog-rtx4080.jpg",
            Price = 1299.00m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Canon EOS R7",
            Category = ["Electronics", "Cameras"],
            Description = "Mirrorless camera with high-speed autofocus and 4K video.",
            ImageFile = "canon-r7.jpg",
            Price = 1499.95m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Amazon Echo Dot (5th Gen)",
            Category = ["Smart Home", "Speakers"],
            Description = "Compact smart speaker with Alexa and improved sound.",
            ImageFile = "echo-dot.jpg",
            Price = 49.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Fitbit Charge 6",
            Category = ["Wearables", "Fitness"],
            Description = "Advanced fitness tracker with heart-rate and GPS.",
            ImageFile = "fitbit-charge6.jpg",
            Price = 149.95m
        }
    ];

}
