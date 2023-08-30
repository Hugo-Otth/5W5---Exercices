using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cour2._1_Ex.Data;
using Cour2._1_Ex.Models;

namespace Cour2._1_Ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> GetCat()
        {
          if (_context.Cat == null)
          {
              return NotFound();
          }
            return await _context.Cat.ToListAsync();
        }

        // GET: api/Cats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(int id)
        {
          if (_context.Cat == null)
          {
              return NotFound();
          }
            var cat = await _context.Cat.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        // PUT: api/Cats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(int id, Cat cat)
        {
            if (id != cat.id)
            {
                return BadRequest();
            }

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        // POST: api/Cats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cat>> PostCat(Cat cat)
        {
          if (_context.Cat == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Cat'  is null.");
          }
            _context.Cat.Add(cat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCat", new { id = cat.id }, cat);
        }

        // DELETE: api/Cats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            if (_context.Cat == null)
            {
                return NotFound();
            }
            var cat = await _context.Cat.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Cat.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatExists(int id)
        {
            return (_context.Cat?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
