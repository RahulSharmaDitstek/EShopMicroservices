namespace Discount.Grpc.Services;

public class DiscountService(ILogger<DiscountService> logger, DiscountContext dbContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Creating discount for product: {ProductName}", request.Coupon.ProductName);

            var existingCoupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.Coupon.ProductName, context.CancellationToken);

            if (existingCoupon != null)
            {
                logger.LogWarning("Coupon for product {ProductName} already exists", request.Coupon.ProductName);
                throw new RpcException(new Status(StatusCode.AlreadyExists, $"Coupon for product {request.Coupon.ProductName} already exists"));
            }
            var coupon = new Coupon
            {
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount
            };
            dbContext.Coupons.Add(coupon);
            dbContext.SaveChanges();
            logger.LogInformation("Coupon for product {ProductName} created successfully", request.Coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while processing the request.");
            throw;
        }

    }
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Getting discount for product: {ProductName}", request.ProductName);

            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

            if (coupon is null)
            {
                logger.LogWarning("Coupon for product {ProductName} not found", request.ProductName);
                coupon = new Coupon
                {
                    ProductName = "No Discount",
                    Description = "No Discount Description",
                    Amount = 0.00m
                };
            }

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while processing the request.");
            throw;
        }
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Updating discount for product: {ProductName}", request.Coupon.ProductName);

            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.Coupon.ProductName);

            if (coupon is null)
            {
                logger.LogWarning("Coupon for product {ProductName} not found", request.Coupon.ProductName);
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon for product {request.Coupon.ProductName} not found"));
            }
            coupon.Description = request.Coupon.Description;
            coupon.Amount = request.Coupon.Amount;
            dbContext.Coupons.Update(coupon);
            dbContext.SaveChanges();

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while processing the request.");
            throw;
        }
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Deleting discount for product: {ProductName}", request.ProductName);
            var coupon = dbContext
                .Coupons
                .FirstOrDefault(c => c.ProductName == request.ProductName);

            if (coupon is null)
            {
                logger.LogWarning("Coupon for product {ProductName} not found", request.ProductName);
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon for product {request.ProductName} not found"));
            }
            logger.LogInformation("Coupon for product {ProductName} found, proceeding to delete", request.ProductName);

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync(context.CancellationToken);

            return new DeleteDiscountResponse { Success = true };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while processing the request.");
            throw;
        }
    }
}
