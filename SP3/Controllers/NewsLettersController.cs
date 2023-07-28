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
    public class NewsLettersController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public NewsLettersController(SocialWelfareContext context)
        {
            _context = context;
        }

        [HttpPost("check-email")]
        public IActionResult CheckEmailExists([FromBody] CheckEmailExistsRequest request)
        {
            // Check if the email exists in the database
            bool emailExists = _context.NewsLetters.Any(s => s.Email == request.Email);

            return Ok(new { exists = emailExists });
        }


        // GET: api/NewsLetters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsLetter>>> GetNewsLetters()
        {
          if (_context.NewsLetters == null)
          {
              return NotFound();
          }
            return await _context.NewsLetters.ToListAsync();
        }

        // GET: api/NewsLetters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsLetter>> GetNewsLetter(int id)
        {
          if (_context.NewsLetters == null)
          {
              return NotFound();
          }
            var newsLetter = await _context.NewsLetters.FindAsync(id);

            if (newsLetter == null)
            {
                return NotFound();
            }

            return newsLetter;
        }

        // PUT: api/NewsLetters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsLetter(int id, NewsLetter newsLetter)
        {
            if (id != newsLetter.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsLetter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsLetterExists(id))
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

        // POST: api/NewsLetters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsLetter>> PostNewsLetter(NewsLetter newsLetter)
        {
          if (_context.NewsLetters == null)
          {
              return Problem("Entity set 'SocialWelfareContext.NewsLetters'  is null.");
          }
            _context.NewsLetters.Add(newsLetter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsLetter", new { id = newsLetter.Id }, newsLetter);
        }

        // DELETE: api/NewsLetters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsLetter(int id)
        {
            if (_context.NewsLetters == null)
            {
                return NotFound();
            }
            var newsLetter = await _context.NewsLetters.FindAsync(id);
            if (newsLetter == null)
            {
                return NotFound();
            }

            _context.NewsLetters.Remove(newsLetter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsLetterExists(int id)
        {
            return (_context.NewsLetters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
