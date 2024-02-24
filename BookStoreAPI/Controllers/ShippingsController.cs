using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IRepository<Shipping> _shippingRepository;

        public ShippingController(IRepository<Shipping> shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        // GET: api/Shipping
        [HttpGet]
        public IActionResult GetShippings()
        {
            var shippings = _shippingRepository.GetAll();
            return Ok(shippings);
        }

        // GET: api/Shipping/5
        [HttpGet("{id}")]
        public IActionResult GetShipping(int id)
        {
            var shipping = _shippingRepository.Find(id);

            if (shipping == null)
            {
                return NotFound();
            }

            return Ok(shipping);
        }

        // POST: api/Shipping
        [HttpPost]
        public IActionResult PostShipping([FromBody] Shipping shipping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shippingRepository.Add(shipping);
            _shippingRepository.SaveChange();

            return CreatedAtAction("GetShipping", new { id = shipping.ShippingId }, shipping);
        }

        // PUT: api/Shipping/5
        [HttpPut("{id}")]
        public IActionResult PutShipping(int id, [FromBody] Shipping shipping)
        {
            if (id != shipping.ShippingId)
            {
                return BadRequest();
            }

            try
            {
                _shippingRepository.Update(shipping);
                _shippingRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_shippingRepository.Find(id) == null)
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

        // DELETE: api/Shipping/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShipping(int id)
        {
            var shipping = _shippingRepository.Find(id);
            if (shipping == null)
            {
                return NotFound();
            }

            _shippingRepository.Delete(id);
            _shippingRepository.SaveChange();

            return Ok(shipping);
        }
    }
}