using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; } = new List<CouponRedemption>();

    public virtual ICollection<FeedbackAndQuery> FeedbackAndQueries { get; set; } = new List<FeedbackAndQuery>();

    public virtual ICollection<NotificationMessage> NotificationMessageAdminUsers { get; set; } = new List<NotificationMessage>();

    public virtual ICollection<NotificationMessage> NotificationMessageRecipientUsers { get; set; } = new List<NotificationMessage>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
}
