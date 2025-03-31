using Discount.Grpc.Models;

namespace Discount.Grpc.Mapper;

public static class CouponMapper
{
    public static CouponModel ToResponse(this Coupon coupon) => new()
    {
        Id = coupon.Id,
        ProductName = coupon.ProductName,
        Description = coupon.Description,
        Amount = coupon.Amount
    };

    public static Coupon ToModel(this CouponModel model) => new()
    {
        Id = model.Id,
        ProductName = model.ProductName,
        Description = model.Description,
        Amount = model.Amount
    };
}