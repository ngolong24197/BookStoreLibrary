using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class FeedbackAndQuery
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int? ProductId { get; set; }

    public string Content { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public DateTime DateSubmitted { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User User { get; set; } = null!;
}
