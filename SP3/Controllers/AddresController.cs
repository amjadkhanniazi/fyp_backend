using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP3.Model;

namespace SP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AddresController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public AddresController(SocialWelfareContext context)
        {
            _context = context;
        }

        // GET: api/Addres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addre>>> GetAddres()
        {
          if (_context.Addres == null)
          {
              return NotFound();
          }
            return await _context.Addres.ToListAsync();
        }

        // GET: api/Addres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addre>> GetAddre(int id)
        {
          if (_context.Addres == null)
          {
              return NotFound();
          }
            var addre = await _context.Addres.FindAsync(id);

            if (addre == null)
            {
                return NotFound();
            }

            return addre;
        }

        // PUT: api/Addres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddre(int id, Addre addre)
        {
            if (id != addre.Id)
            {
                return BadRequest();
            }

            _context.Entry(addre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddreExists(id))
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

        // POST: api/Addres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Addre>> PostAddre(Addre addre)
        {
          if (_context.Addres == null)
          {
              return Problem("Entity set 'SocialWelfareContext.Addres'  is null.");
          }
            _context.Addres.Add(addre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddre", new { id = addre.Id }, addre);
        }

        // DELETE: api/Addres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddre(int id)
        {
            if (_context.Addres == null)
            {
                return NotFound();
            }
            var addre = await _context.Addres.FindAsync(id);
            if (addre == null)
            {
                return NotFound();
            }

            _context.Addres.Remove(addre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddreExists(int id)
        {
            return (_context.Addres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
