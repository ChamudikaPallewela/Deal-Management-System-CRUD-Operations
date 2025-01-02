using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ASE.Data;
using ASE.Models;

using System;
using Microsoft.EntityFrameworkCore;

namespace ASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/deals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deal>>> GetDeals()
        {
            return await _context.Deals.Include(d => d.Hotels).ToListAsync();
        }

        // GET: api/deals/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> GetDeal(int id)
        {
            var deal = await _context.Deals.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id == id);
            if (deal == null) return NotFound();
            return deal;
        }

        // POST: api/deals
        [HttpPost]
        public async Task<ActionResult<Deal>> CreateDeal(Deal deal)
        {
            _context.Deals.Add(deal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeal), new { id = deal.Id }, deal);
        }

        // PUT: api/deals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeal(int id, Deal deal)
        {
            if (id != deal.Id) return BadRequest();
            _context.Entry(deal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/deals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal(int id)
        {
            var deal = await _context.Deals.FindAsync(id);
            if (deal == null) return NotFound();
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

