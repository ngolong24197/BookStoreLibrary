using BookStoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryBookMVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string manufacturersApiUrl;


        public ManufacturersController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            manufacturersApiUrl = "https://localhost:7122/api/Manufacturer";
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(manufacturersApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Manufacturer> manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(strData, options);
                return View(manufacturers);
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{manufacturersApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Manufacturer manufacturer = JsonSerializer.Deserialize<Manufacturer>(strData, options);
                return View(manufacturer);
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(manufacturer);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(manufacturersApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Manufacturer inserted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while calling Web API";
                }
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{manufacturersApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Manufacturer manufacturer = JsonSerializer.Deserialize<Manufacturer>(strData, options);
                return View(manufacturer);
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(manufacturer);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{manufacturersApiUrl}/{id}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Manufacturer updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Error while calling Web API";
                }
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{manufacturersApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Manufacturer manufacturer = JsonSerializer.Deserialize<Manufacturer>(strData, options);
                return View(manufacturer);
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{manufacturersApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Manufacturer deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Message"] = "Error while calling Web API";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}