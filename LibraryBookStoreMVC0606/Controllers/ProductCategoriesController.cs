using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreLibrary.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LibraryBookMVC.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string ProductCategoriesApiUrl = "";
        public ProductCategoriesController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ProductCategoriesApiUrl = "https://localhost:7122/api/ProductsCategory";

        }
        // GET: BookController
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage res = await _httpClient.GetAsync(ProductCategoriesApiUrl);
            string strData = await res.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ProductCategory> list = JsonSerializer.Deserialize<List<ProductCategory>>(strData, option);
            return View(list);
        }
        // GET:BookController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{ProductCategoriesApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ProductCategory p = JsonSerializer.Deserialize<ProductCategory>(strData, options);
                return View(p);
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(ProductCategoriesApiUrl, contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Book inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // In mã trạng thái HTTP
                    Console.WriteLine(res.StatusCode);

                    // Đọc nội dung phản hồi (nếu cần)
                    string errorContent = await res.Content.ReadAsStringAsync();
                    Console.WriteLine(errorContent);
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{ProductCategoriesApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ProductCategory p = JsonSerializer.Deserialize<ProductCategory>(strData, options);
                return View(p);
            }
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategory p)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(p);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync($"{ProductCategoriesApiUrl}/{id}", contentData);
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Book updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while call Web API";
                }
            }
            return View(p);
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{ProductCategoriesApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                ProductCategory p = JsonSerializer.Deserialize<ProductCategory>(strData, options);
                return View(p);
            }
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{ProductCategoriesApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                TempData["Message"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "Error while call Web API";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
