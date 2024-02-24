﻿using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Models { 

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string ManufacturerCode { get; set; } = null!;

    public string ManufacturerName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
}