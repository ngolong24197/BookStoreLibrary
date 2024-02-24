using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public int OrderId { get; set; }

    public string? TrackingId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? EstimatedDeliveryDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
