using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models;

public partial class Faq
{
    public int Faqid { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual ProductCategory? Category { get; set; }
}
