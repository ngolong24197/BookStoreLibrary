using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int? CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal? DeliveryCharge { get; set; }

    public string PaymentType { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; } = new List<CouponRedemption>();

    public virtual User? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
