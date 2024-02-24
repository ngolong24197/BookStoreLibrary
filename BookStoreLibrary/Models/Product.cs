using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStoreLibrary.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int? ManufacturerId { get; set; }

    public int? CategoryId { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }
    [JsonIgnore]
    public virtual ProductCategory? Category { get; set; }

    public virtual ICollection<FeedbackAndQuery> FeedbackAndQueries { get; set; } = new List<FeedbackAndQuery>();
    [JsonIgnore]
    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

}
