using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCategoryController : ControllerBase
    {
        private readonly IRepository<ProductCategory> _productCateGoryRepository;

        public ProductsCategoryController(IRepository<ProductCategory> productCategoryRepository)
        {
            _productCateGoryRepository = productCategoryRepository;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProductsCategory()
        {
            var products = _productCateGoryRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProductCategory(int id)
        {
            var product = _productCateGoryRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult PostProduct([FromBody] ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productCateGoryRepository.Add(product);
            _productCateGoryRepository.SaveChange();

            return CreatedAtAction("GetProduct", new { id = product.CategoryId }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult PutProductCategory(int id, [FromBody] ProductCategory product)
        {
            if (id != product.CategoryId)
            {
                return BadRequest();
            }

            _productCateGoryRepository.Update(product);
            try
            {
                _productCateGoryRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_productCateGoryRepository.Find(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProductCategory(int id)
        {
            var product = _productCateGoryRepository.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _productCateGoryRepository.Delete(id);
            _productCateGoryRepository.SaveChange();

            return Ok(product);
        }
    }
}