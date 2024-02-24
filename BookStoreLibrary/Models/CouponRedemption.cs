using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class CouponRedemption
{
    public int RedemptionId { get; set; }

    public int CouponId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public DateTime RedemptionDate { get; set; }

    public virtual Coupon Coupon { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
