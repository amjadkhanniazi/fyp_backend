using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP3.Model;

namespace SP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistriesController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public UserRegistriesController(SocialWelfareContext context)
        {
            _context = context;
        }

        // GET: api/UserRegistries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistry>>> GetUserRegistries()
        {
          if (_context.UserRegistries == null)
          {
              return NotFound();
          }
            return await _context.UserRegistries.ToListAsync();
        }

        // GET: api/UserRegistries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistry>> GetUserRegistry(long id)
        {
          if (_context.UserRegistries == null)
          {
              return NotFound();
          }
            var userRegistry = await _context.UserRegistries.FindAsync(id);

            if (userRegistry == null)
            {
                return NotFound();
            }

            return userRegistry;
        }

        // PUT: api/UserRegistries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRegistry(long id, UserRegistry userRegistry)
        {
            if (id != userRegistry.Cnic)
            {
                return BadRequest();
            }

            _context.Entry(userRegistry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegistryExists(id))
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

        // POST: api/UserRegistries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegistry>> PostUserRegistry(UserRegistry userRegistry)
        {
          if (_context.UserRegistries == null)
          {
              return Problem("Entity set 'SocialWelfareContext.UserRegistries'  is null.");
          }
            _context.UserRegistries.Add(userRegistry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRegistryExists(userRegistry.Cnic))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRegistry", new { id = userRegistry.Cnic }, userRegistry);
        }

        // DELETE: api/UserRegistries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRegistry(long id)
        {
            if (_context.UserRegistries == null)
            {
                return NotFound();
            }
            var userRegistry = await _context.UserRegistries.FindAsync(id);
            if (userRegistry == null)
            {
                return NotFound();
            }

            _context.UserRegistries.Remove(userRegistry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRegistryExists(long id)
        {
            return (_context.UserRegistries?.Any(e => e.Cnic == id)).GetValueOrDefault();
        }
    }
}
