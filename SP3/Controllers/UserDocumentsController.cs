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
    [Authorize]
    public class UserDocumentsController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public UserDocumentsController(SocialWelfareContext context)
        {
            _context = context;
        }

        // GET: api/UserDocuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDocument>>> GetUserDocuments()
        {
          if (_context.UserDocuments == null)
          {
              return NotFound();
          }
            return await _context.UserDocuments.ToListAsync();
        }

        // GET: api/UserDocuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDocument>> GetUserDocument(int id)
        {
          if (_context.UserDocuments == null)
          {
              return NotFound();
          }
            var userDocument = await _context.UserDocuments.FindAsync(id);

            if (userDocument == null)
            {
                return NotFound();
            }

            return userDocument;
        }

        // PUT: api/UserDocuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDocument(int id, UserDocument userDocument)
        {
            if (id != userDocument.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDocumentExists(id))
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

        // POST: api/UserDocuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDocument>> PostUserDocument(UserDocument userDocument)
        {
          if (_context.UserDocuments == null)
          {
              return Problem("Entity set 'SocialWelfareContext.UserDocuments'  is null.");
          }
            _context.UserDocuments.Add(userDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDocument", new { id = userDocument.Id }, userDocument);
        }

        // DELETE: api/UserDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDocument(int id)
        {
            if (_context.UserDocuments == null)
            {
                return NotFound();
            }
            var userDocument = await _context.UserDocuments.FindAsync(id);
            if (userDocument == null)
            {
                return NotFound();
            }

            _context.UserDocuments.Remove(userDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDocumentExists(int id)
        {
            return (_context.UserDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
