namespace Shopping.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}& pageSize={pageSize}")]
    Task<GetOrderResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);

    [Get("/ordering-service/orders/{orderName}")]
    Task<GetOrdersByNameResponse> GetOrderByName(string orderName);

    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrdersByCustomerResponse> GetOrderByCustomer(Guid customerId);

}
//public class ApiSettings
//{
//    public string GatewayAddress { get; set; } = string.Empty;
//}


public static class AppConstants
{
        public static string GatewayAddress { get; set; } = string.Empty;


        public const string CatalogBase = "catalog-service/products";
        public static string ProductDetails(string productId)
       => $"{CatalogBase}/{productId.Trim()}";
        public static string ProductsByCategory(string category)
    => $"{CatalogBase}/category/{category.Trim()}";
        public const string OrderingBase = "ordering-service/orders";
        public static string OrdersByName(string orderName)
            => $"{OrderingBase}/{orderName.Trim()}";
        public static string OrdersByCustomer(Guid customerId)
            => $"{OrderingBase}/customer/{customerId}";
    }

