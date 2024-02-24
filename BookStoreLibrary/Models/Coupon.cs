using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string CouponCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Discount { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int TotalUses { get; set; }

    public int RemainingUses { get; set; }

    public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; } = new List<CouponRedemption>();

    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
}
