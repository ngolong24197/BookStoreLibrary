using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IRepository<Faq> _faqRepository;

        public FAQController(IRepository<Faq> faqRepository)
        {
            _faqRepository = faqRepository;
        }

        // GET: api/FAQ
        [HttpGet]
        public IActionResult GetFAQs()
        {
            var faqs = _faqRepository.GetAll();
            return Ok(faqs);
        }

        // GET: api/FAQ/5
        [HttpGet("{id}")]
        public IActionResult GetFAQ(int id)
        {
            var faq = _faqRepository.Find(id);

            if (faq == null)
            {
                return NotFound();
            }

            return Ok(faq);
        }

        // POST: api/FAQ
        [HttpPost]
        public IActionResult PostFAQ([FromBody] Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _faqRepository.Add(faq);
            _faqRepository.SaveChange();

            return CreatedAtAction("GetFAQ", new { id = faq.Faqid }, faq);
        }

        // PUT: api/FAQ/5
        [HttpPut("{id}")]
        public IActionResult PutFAQ(int id, [FromBody] Faq faq)
        {
            if (id != faq.Faqid)
            {
                return BadRequest();
            }

            try
            {
                _faqRepository.Update(faq);
                _faqRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_faqRepository.Find(id) == null)
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

        // DELETE: api/FAQ/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFAQ(int id)
        {
            var faq = _faqRepository.Find(id);
            if (faq == null)
            {
                return NotFound();
            }

            _faqRepository.Delete(id);
            _faqRepository.SaveChange();

            return Ok(faq);
        }
    }
}