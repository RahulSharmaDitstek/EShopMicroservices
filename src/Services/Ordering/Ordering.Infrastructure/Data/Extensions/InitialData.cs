namespace Ordering.Infrastructure.Data.Extensions;

public static class InitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111")), "Alice Smith", "alice.smith@example.com"),
        Customer.Create(CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222")), "Bob Johnson", "bob.johnson@example.com"),
        Customer.Create(CustomerId.Of(Guid.Parse("33333333-3333-3333-3333-333333333333")), "Charlie Brown", "charlie.brown@example.com")
    ];

    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), "Laptop", 1200.00m),
        Product.Create(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), "Smartphone", 800.00m),
        Product.Create(ProductId.Of(Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")), "Headphones", 150.00m)
    ];

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Alice", "Smith", "alice.smith@example.com", "123 Main St", "USA", "NY", "10001");
            var address2 = Address.Of("Bob", "Johnson", "bob.johnson@example.com", "456 Elm St", "USA", "CA", "90001");
            var address3 = Address.Of("Charlie", "Brown", "charlie.brown@example.com", "789 Oak St", "USA", "TX", "73301");

            var payment1 = Payment.Of("Alice Smith", "4111111111111111", "12/25", "123", 1);
            var payment2 = Payment.Of("Bob Johnson", "4222222222222222", "11/26", "456", 2);
            var payment3 = Payment.Of("Charlie Brown", "4333333333333333", "10/27", "789", 1);

            var order1 = Order.CreateOrderItem(
                OrderId.Of(Guid.Parse("44444444-4444-4444-4444-444444444444")),
                CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111")),
                OrderName.Of("Order #1"),
                address1,
                address1,
                payment1
            );
            order1.AddOrderItem(ProductId.Of(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 1, 1200.00m);
            order1.AddOrderItem(ProductId.Of(Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")), 2, 150.00m);

            var order2 = Order.CreateOrderItem(
                OrderId.Of(Guid.Parse("55555555-5555-5555-5555-555555555555")),
                CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222")),
                OrderName.Of("Order #2"),
                address2,
                address2,
                payment2
            );
            order2.AddOrderItem(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 1, 800.00m);

            var order3 = Order.CreateOrderItem(
                OrderId.Of(Guid.Parse("66666666-6666-6666-6666-666666666666")),
                CustomerId.Of(Guid.Parse("33333333-3333-3333-3333-333333333333")),
                OrderName.Of("Order #3"),
                address3,
                address3,
                payment3
            );
            order3.AddOrderItem(ProductId.Of(Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")), 3, 150.00m);

            var order4 = Order.CreateOrderItem(
                OrderId.Of(Guid.Parse("77777777-7777-7777-7777-777777777777")),
                CustomerId.Of(Guid.Parse("11111111-1111-1111-1111-111111111111")),
                OrderName.Of("Order #4"),
                address1,
                address1,
                payment1
            );
            order4.AddOrderItem(ProductId.Of(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 2, 800.00m);

            var order5 = Order.CreateOrderItem(
                OrderId.Of(Guid.Parse("88888888-8888-8888-8888-888888888888")),
                CustomerId.Of(Guid.Parse("22222222-2222-2222-2222-222222222222")),
                OrderName.Of("Order #5"),
                address2,
                address2,
                payment2
            );
            order5.AddOrderItem(ProductId.Of(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 1, 1200.00m);
            order5.AddOrderItem(ProductId.Of(Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")), 1, 150.00m);

            return [order1, order2, order3, order4, order5];
        }
    }
}