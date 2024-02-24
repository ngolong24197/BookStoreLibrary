using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStoreLibrary.Models;

namespace BookStoreLibrary.ViewModels;

public class CreateProductViewModel
{
    [Required]
    [Display(Name = "Product Code")]
    public string ProductCode { get; set; }

    [Required]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Manufacturer")]
    public int? ManufacturerId { get; set; }

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }

    // These lists will be used to populate dropdowns in the UI
    public IEnumerable<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
    public IEnumerable<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
}