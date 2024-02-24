using BookStoreLibrary.Models;
using BookStoreLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleController(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/Role
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleRepository.GetAll();
            return Ok(roles);
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var role = _roleRepository.Find(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST: api/Role
        [HttpPost]
        public IActionResult PostRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _roleRepository.Add(role);
            _roleRepository.SaveChange();

            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public IActionResult PutRole(int id, [FromBody] Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _roleRepository.Update(role);
            try
            {
                _roleRepository.SaveChange();
            }
            catch (Exception)
            {
                if (_roleRepository.Find(id) == null)
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

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _roleRepository.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            _roleRepository.Delete(id);
            _roleRepository.SaveChange();

            return Ok(role);
        }
    }
}