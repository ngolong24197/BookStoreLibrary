using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookStoreLibrary.Models; 
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreLibrary.Repository;

namespace LibraryBookStoreMVC0606.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string ProductApiUrl = "https://localhost:7122/api/Products";
        private readonly string CategoryApiUrl = "https://localhost:7122/api/ProductsCategory";
        private readonly string ManufacturerApiUrl = "https://localhost:7122/api/Manufacturer";

        public ProductsController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Product
        // GET: Product
        public async Task<IActionResult> Index(int? page, int pageSize = 5)
        {
             var pageNumber = page ?? 1;
            // Fetch products
            var productResponse = await _httpClient.GetAsync($"{ProductApiUrl}");
            var products = new List<Product>();

            if (productResponse.IsSuccessStatusCode)
            {
                var productContent = await productResponse.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(productContent);
            }
            else
            {
                products = new List<Product>();
            }

            // Fetch categories
            var categoryResponse = await _httpClient.GetAsync($"{CategoryApiUrl}");
            var categories = new List<ProductCategory>();

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoryContent);
            }

            // Fetch manufacturers
            var manufacturerResponse = await _httpClient.GetAsync($"{ManufacturerApiUrl}");
            var manufacturers = new List<Manufacturer>();

            if (manufacturerResponse.IsSuccessStatusCode)
            {
                var manufacturerContent = await manufacturerResponse.Content.ReadAsStringAsync();
                manufacturers = JsonConvert.DeserializeObject<List<Manufacturer>>(manufacturerContent);
            }

            // Merge products with categories and manufacturers
            foreach (var product in products)
            {
                product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                product.Manufacturer = manufacturers.FirstOrDefault(m => m.ManufacturerId == product.ManufacturerId);
            }
            var paginatedProducts = PaginatedList<Product>.Create(products.AsQueryable(), pageNumber, pageSize);
            return View(paginatedProducts);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(content);
                return View(product);
            }
            return NotFound();
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesDropDownList();
            await PopulateManufacturersDropDownList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductCode,ProductName,Description,ManufacturerId,CategoryId,Price,ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"{ProductApiUrl}", product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            await PopulateCategoriesDropDownList(product.CategoryId);
            await PopulateManufacturersDropDownList(product.ManufacturerId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(content);
                await PopulateCategoriesDropDownList(product.CategoryId);
                await PopulateManufacturersDropDownList(product.ManufacturerId);
                return View(product);
            }
            return NotFound();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductCode,ProductName,Description,ManufacturerId,CategoryId,Price,ImageUrl")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"{ProductApiUrl}/{id}", product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            await PopulateCategoriesDropDownList(product.CategoryId);
            await PopulateManufacturersDropDownList(product.ManufacturerId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(content);
                return View(product);
            }
            return NotFound();
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ProductApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private async Task PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var response = await _httpClient.GetAsync($"{CategoryApiUrl}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(content);
                ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", selectedCategory);
            }
        }

        private async Task PopulateManufacturersDropDownList(object selectedManufacturer = null)
        {
            var response = await _httpClient.GetAsync($"{ManufacturerApiUrl}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var manufacturers = JsonConvert.DeserializeObject<List<Manufacturer>>(content);
                ViewBag.ManufacturerId = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName", selectedManufacturer);
            }
        }
    }
}