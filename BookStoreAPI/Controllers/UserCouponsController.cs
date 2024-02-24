using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreLibrary.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCouponsController : ControllerBase
    {
        private readonly ShradhaBookStoresContext _context;

        public UserCouponsController(ShradhaBookStoresContext context)
        {
            _context = context;
        }

        // GET: api/UserCoupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCoupon>>> GetUserCoupons()
        {
            return await _context.UserCoupons.ToListAsync();
        }

        // GET: api/UserCoupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCoupon>> GetUserCoupon(int id)
        {
            var userCoupon = await _context.UserCoupons.FindAsync(id);

            if (userCoupon == null)
            {
                return NotFound();
            }

            return userCoupon;
        }

        // PUT: api/UserCoupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCoupon(int id, UserCoupon userCoupon)
        {
            if (id != userCoupon.UserCouponId)
            {
                return BadRequest();
            }

            _context.Entry(userCoupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCouponExists(id))
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

        // POST: api/UserCoupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCoupon>> PostUserCoupon(UserCoupon userCoupon)
        {
            _context.UserCoupons.Add(userCoupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCoupon", new { id = userCoupon.UserCouponId }, userCoupon);
        }

        // DELETE: api/UserCoupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCoupon(int id)
        {
            var userCoupon = await _context.UserCoupons.FindAsync(id);
            if (userCoupon == null)
            {
                return NotFound();
            }

            _context.UserCoupons.Remove(userCoupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCouponExists(int id)
        {
            return _context.UserCoupons.Any(e => e.UserCouponId == id);
        }
    }
}
