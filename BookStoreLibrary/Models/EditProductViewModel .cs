using BookStoreLibrary.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryBookMVC.ViewModels
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Code is required.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Manufacturer is required.")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        // New properties for displaying related data
        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}