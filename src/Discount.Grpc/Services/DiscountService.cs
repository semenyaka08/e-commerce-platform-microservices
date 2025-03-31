using Discount.Grpc.Data;
using Discount.Grpc.Mapper;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(z=>z.ProductName == request.ProductName) ?? new Coupon
        {
            ProductName = "No product name",
            Description = "No description",
            Amount = 0
        };
        
        logger.LogInformation("Getting coupon for product: {productName}", coupon.ProductName);

        return coupon.ToResponse();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var mappedCoupon = request.Coupon.ToModel();
        
        if(mappedCoupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        await dbContext.Coupons.AddAsync(mappedCoupon);

        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Coupon with for product: {productName} with Id: {id}", mappedCoupon.ProductName, mappedCoupon.Id);

        return mappedCoupon.ToResponse();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.ToModel();

        if(coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        
        dbContext.Coupons.Update(coupon);
        
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Coupon with id: {id} was successfully updated", coupon.Id);

        return coupon.ToResponse();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(z=>z.ProductName == request.ProductName);
        
        if(coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with product name: {request.ProductName} was not found"));

        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Coupon with product name: {request.ProductName} was successfully removed", request.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}