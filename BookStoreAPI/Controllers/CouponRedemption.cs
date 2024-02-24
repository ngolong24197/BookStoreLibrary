using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponRedemptionController : ControllerBase
    {
        private readonly IRepository<CouponRedemption> _couponRedemptionRepository;

        public CouponRedemptionController(IRepository<CouponRedemption> couponRedemptionRepository)
        {
            _couponRedemptionRepository = couponRedemptionRepository;
        }

        // GET: api/CouponRedemption
        [HttpGet]
        public IActionResult GetCouponRedemptions()
        {
            var couponRedemptions = _couponRedemptionRepository.GetAll();
            return Ok(couponRedemptions);
        }

        // GET: api/CouponRedemption/5
        [HttpGet("{id}")]
        public IActionResult GetCouponRedemption(int id)
        {
            var couponRedemption = _couponRedemptionRepository.Find(id);

            if (couponRedemption == null)
            {
                return NotFound();
            }

            return Ok(couponRedemption);
        }

        // POST: api/CouponRedemption
        [HttpPost]
        public IActionResult PostCouponRedemption([FromBody] CouponRedemption couponRedemption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _couponRedemptionRepository.Add(couponRedemption);
            _couponRedemptionRepository.SaveChange();

            return CreatedAtAction("GetCouponRedemption", new { id = couponRedemption.RedemptionId }, couponRedemption);
        }

        // PUT: api/CouponRedemption/5
        [HttpPut("{id}")]
        public IActionResult PutCouponRedemption(int id, [FromBody] CouponRedemption couponRedemption)
        {
            if (id != couponRedemption.RedemptionId)
            {
                return BadRequest();
            }

            try
            {
                _couponRedemptionRepository.Update(couponRedemption);
                _couponRedemptionRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_couponRedemptionRepository.Find(id) == null)
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

        // DELETE: api/CouponRedemption/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCouponRedemption(int id)
        {
            var couponRedemption = _couponRedemptionRepository.Find(id);
            if (couponRedemption == null)
            {
                return NotFound();
            }

            _couponRedemptionRepository.Delete(id);
            _couponRedemptionRepository.SaveChange();

            return Ok(couponRedemption);
        }
    }
}