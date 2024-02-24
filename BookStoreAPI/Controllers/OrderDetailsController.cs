using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public OrderDetailController(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        // GET: api/OrderDetail
        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            var orderDetails = _orderDetailRepository.GetAll();
            return Ok(orderDetails);
        }

        // GET: api/OrderDetail/5
        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var orderDetail = _orderDetailRepository.Find(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        // POST: api/OrderDetail
        [HttpPost]
        public IActionResult PostOrderDetail([FromBody] OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _orderDetailRepository.Add(orderDetail);
            _orderDetailRepository.SaveChange();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // PUT: api/OrderDetail/5
        [HttpPut("{id}")]
        public IActionResult PutOrderDetail(int id, [FromBody] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            _orderDetailRepository.Update(orderDetail);
            try
            {
                _orderDetailRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_orderDetailRepository.Find(id) == null)
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

        // DELETE: api/OrderDetail/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var orderDetail = _orderDetailRepository.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _orderDetailRepository.Delete(id);
            _orderDetailRepository.SaveChange();

            return Ok(orderDetail);
        }
    }
}