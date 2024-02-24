using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class NotificationMessage
{
    public int NotificationId { get; set; }

    public int AdminUserId { get; set; }

    public int RecipientUserId { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime DateSent { get; set; }

    public virtual User AdminUser { get; set; } = null!;

    public virtual User RecipientUser { get; set; } = null!;
}
