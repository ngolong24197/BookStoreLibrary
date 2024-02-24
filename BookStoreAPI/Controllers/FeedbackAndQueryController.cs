using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackAndQueryController : ControllerBase
    {
        private readonly IRepository<FeedbackAndQuery> _feedbackAndQueryRepository;

        public FeedbackAndQueryController(IRepository<FeedbackAndQuery> feedbackAndQueryRepository)
        {
            _feedbackAndQueryRepository = feedbackAndQueryRepository;
        }

        // GET: api/FeedbackAndQuery
        [HttpGet]
        public IActionResult GetFeedbackAndQueries()
        {
            var feedbackAndQueries = _feedbackAndQueryRepository.GetAll();
            return Ok(feedbackAndQueries);
        }

        // GET: api/FeedbackAndQuery/5
        [HttpGet("{id}")]
        public IActionResult GetFeedbackAndQuery(int id)
        {
            var feedbackAndQuery = _feedbackAndQueryRepository.Find(id);

            if (feedbackAndQuery == null)
            {
                return NotFound();
            }

            return Ok(feedbackAndQuery);
        }

        // POST: api/FeedbackAndQuery
        [HttpPost]
        public IActionResult PostFeedbackAndQuery([FromBody] FeedbackAndQuery feedbackAndQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _feedbackAndQueryRepository.Add(feedbackAndQuery);
            _feedbackAndQueryRepository.SaveChange();

            return CreatedAtAction("GetFeedbackAndQuery", new { id = feedbackAndQuery.FeedbackId }, feedbackAndQuery);
        }

        // PUT: api/FeedbackAndQuery/5
        [HttpPut("{id}")]
        public IActionResult PutFeedbackAndQuery(int id, [FromBody] FeedbackAndQuery feedbackAndQuery)
        {
            if (id != feedbackAndQuery.FeedbackId)
            {
                return BadRequest();
            }

            try
            {
                _feedbackAndQueryRepository.Update(feedbackAndQuery);
                _feedbackAndQueryRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_feedbackAndQueryRepository.Find(id) == null)
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

        // DELETE: api/FeedbackAndQuery/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFeedbackAndQuery(int id)
        {
            var feedbackAndQuery = _feedbackAndQueryRepository.Find(id);
            if (feedbackAndQuery == null)
            {
                return NotFound();
            }

            _feedbackAndQueryRepository.Delete(id);
            _feedbackAndQueryRepository.SaveChange();

            return Ok(feedbackAndQuery);
        }
    }
}