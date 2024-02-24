using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IRepository<Coupon> _couponRepository;

        public CouponController(IRepository<Coupon> couponRepository)
        {
            _couponRepository = couponRepository;
        }

        // GET: api/Coupon
        [HttpGet]
        public IActionResult GetCoupons()
        {
            var coupons = _couponRepository.GetAll();
            return Ok(coupons);
        }

        // GET: api/Coupon/5
        [HttpGet("{id}")]
        public IActionResult GetCoupon(int id)
        {
            var coupon = _couponRepository.Find(id);

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        // POST: api/Coupon
        [HttpPost]
        public IActionResult PostCoupon([FromBody] Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _couponRepository.Add(coupon);
            _couponRepository.SaveChange();

            return CreatedAtAction("GetCoupon", new { id = coupon.CouponId }, coupon);
        }

        // PUT: api/Coupon/5
        [HttpPut("{id}")]
        public IActionResult PutCoupon(int id, [FromBody] Coupon coupon)
        {
            if (id != coupon.CouponId)
            {
                return BadRequest();
            }

            try
            {
                _couponRepository.Update(coupon);
                _couponRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_couponRepository.Find(id) == null)
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

        // DELETE: api/Coupon/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCoupon(int id)
        {
            var coupon = _couponRepository.Find(id);
            if (coupon == null)
            {
                return NotFound();
            }

            _couponRepository.Delete(id);
            _couponRepository.SaveChange();

            return Ok(coupon);
        }
    }
}