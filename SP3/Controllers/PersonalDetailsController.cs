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
    public class PersonalDetailsController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public PersonalDetailsController(SocialWelfareContext context)
        {
            _context = context;
        }

        // GET: api/PersonalDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalDetail>>> GetPersonalDetails()
        {
          if (_context.PersonalDetails == null)
          {
              return NotFound();
          }
            return await _context.PersonalDetails.ToListAsync();
        }

        // GET: api/PersonalDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDetail>> GetPersonalDetail(int id)
        {
          if (_context.PersonalDetails == null)
          {
              return NotFound();
          }
            var personalDetail = await _context.PersonalDetails.FindAsync(id);

            if (personalDetail == null)
            {
                return NotFound();
            }

            return personalDetail;
        }

        // PUT: api/PersonalDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalDetail(int id, PersonalDetail personalDetail)
        {
            if (id != personalDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(personalDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalDetailExists(id))
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

        // POST: api/PersonalDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonalDetail>> PostPersonalDetail(PersonalDetail personalDetail)
        {
          if (_context.PersonalDetails == null)
          {
              return Problem("Entity set 'SocialWelfareContext.PersonalDetails'  is null.");
          }
            _context.PersonalDetails.Add(personalDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonalDetailExists(personalDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonalDetail", new { id = personalDetail.Id }, personalDetail);
        }

        // DELETE: api/PersonalDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalDetail(int id)
        {
            if (_context.PersonalDetails == null)
            {
                return NotFound();
            }
            var personalDetail = await _context.PersonalDetails.FindAsync(id);
            if (personalDetail == null)
            {
                return NotFound();
            }

            _context.PersonalDetails.Remove(personalDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalDetailExists(int id)
        {
            return (_context.PersonalDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
