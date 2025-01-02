using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASE.Data;
using ASE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.Hotels.Include(h => h.Deal).ToListAsync();
        }

        // GET: api/hotels/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Include(h => h.Deal).FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
                return NotFound();
            return hotel;
        }

        // POST: api/hotels
        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        // PUT: api/hotels/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
                return BadRequest();

            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/hotels/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound();

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

