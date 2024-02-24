using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationMessageController : ControllerBase
    {
        private readonly IRepository<NotificationMessage> _notificationMessageRepository;

        public NotificationMessageController(IRepository<NotificationMessage> notificationMessageRepository)
        {
            _notificationMessageRepository = notificationMessageRepository;
        }

        // GET: api/NotificationMessage
        [HttpGet]
        public IActionResult GetNotificationMessages()
        {
            var notificationMessages = _notificationMessageRepository.GetAll();
            return Ok(notificationMessages);
        }

        // GET: api/NotificationMessage/5
        [HttpGet("{id}")]
        public IActionResult GetNotificationMessage(int id)
        {
            var notificationMessage = _notificationMessageRepository.Find(id);

            if (notificationMessage == null)
            {
                return NotFound();
            }

            return Ok(notificationMessage);
        }

        // POST: api/NotificationMessage
        [HttpPost]
        public IActionResult PostNotificationMessage([FromBody] NotificationMessage notificationMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _notificationMessageRepository.Add(notificationMessage);
            _notificationMessageRepository.SaveChange();

            return CreatedAtAction("GetNotificationMessage", new { id = notificationMessage.NotificationId }, notificationMessage);
        }

        // PUT: api/NotificationMessage/5
        [HttpPut("{id}")]
        public IActionResult PutNotificationMessage(int id, [FromBody] NotificationMessage notificationMessage)
        {
            if (id != notificationMessage.NotificationId)
            {
                return BadRequest();
            }

            try
            {
                _notificationMessageRepository.Update(notificationMessage);
                _notificationMessageRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_notificationMessageRepository.Find(id) == null)
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

        // DELETE: api/NotificationMessage/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNotificationMessage(int id)
        {
            var notificationMessage = _notificationMessageRepository.Find(id);
            if (notificationMessage == null)
            {
                return NotFound();
            }

            _notificationMessageRepository.Delete(id);
            _notificationMessageRepository.SaveChange();

            return Ok(notificationMessage);
        }
    }
}