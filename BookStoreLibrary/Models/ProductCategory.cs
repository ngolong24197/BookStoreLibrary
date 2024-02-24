using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models { 

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Faq> Faqs { get; set; } = new List<Faq>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
}