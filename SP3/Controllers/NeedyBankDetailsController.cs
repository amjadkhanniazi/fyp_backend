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
    public class NeedyBankDetailsController : ControllerBase
    {
        private readonly SocialWelfareContext _context;

        public NeedyBankDetailsController(SocialWelfareContext context)
        {
            _context = context;
        }

        // GET: api/NeedyBankDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NeedyBankDetail>>> GetNeedyBankDetails()
        {
          if (_context.NeedyBankDetails == null)
          {
              return NotFound();
          }
            return await _context.NeedyBankDetails.ToListAsync();
        }

        // GET: api/NeedyBankDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NeedyBankDetail>> GetNeedyBankDetail(int id)
        {
          if (_context.NeedyBankDetails == null)
          {
              return NotFound();
          }
            var needyBankDetail = await _context.NeedyBankDetails.FindAsync(id);

            if (needyBankDetail == null)
            {
                return NotFound();
            }

            return needyBankDetail;
        }

        // PUT: api/NeedyBankDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNeedyBankDetail(int id, NeedyBankDetail needyBankDetail)
        {
            if (id != needyBankDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(needyBankDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeedyBankDetailExists(id))
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

        // POST: api/NeedyBankDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NeedyBankDetail>> PostNeedyBankDetail(NeedyBankDetail needyBankDetail)
        {
          if (_context.NeedyBankDetails == null)
          {
              return Problem("Entity set 'SocialWelfareContext.NeedyBankDetails'  is null.");
          }
            _context.NeedyBankDetails.Add(needyBankDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNeedyBankDetail", new { id = needyBankDetail.Id }, needyBankDetail);
        }

        // DELETE: api/NeedyBankDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNeedyBankDetail(int id)
        {
            if (_context.NeedyBankDetails == null)
            {
                return NotFound();
            }
            var needyBankDetail = await _context.NeedyBankDetails.FindAsync(id);
            if (needyBankDetail == null)
            {
                return NotFound();
            }

            _context.NeedyBankDetails.Remove(needyBankDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NeedyBankDetailExists(int id)
        {
            return (_context.NeedyBankDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
