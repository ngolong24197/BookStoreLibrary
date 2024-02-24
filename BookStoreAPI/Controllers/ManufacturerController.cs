using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;

        public ManufacturerController(IRepository<Manufacturer> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        // GET: api/Manufacturer
        [HttpGet]
        public IActionResult GetManufacturers()
        {
            var manufacturers = _manufacturerRepository.GetAll();
            return Ok(manufacturers);
        }

        // GET: api/Manufacturer/5
        [HttpGet("{id}")]
        public IActionResult GetManufacturer(int id)
        {
            var manufacturer = _manufacturerRepository.Find(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return Ok(manufacturer);
        }

        // POST: api/Manufacturer
        [HttpPost]
        public IActionResult PostManufacturer([FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _manufacturerRepository.Add(manufacturer);
            _manufacturerRepository.SaveChange();

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // PUT: api/Manufacturer/5
        [HttpPut("{id}")]
        public IActionResult PutManufacturer(int id, [FromBody] Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }

            try
            {
                _manufacturerRepository.Update(manufacturer);
                _manufacturerRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_manufacturerRepository.Find(id) == null)
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

        // DELETE: api/Manufacturer/5
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            var manufacturer = _manufacturerRepository.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _manufacturerRepository.Delete(id);
            _manufacturerRepository.SaveChange();
                
            return Ok(manufacturer);
        }
    }
}