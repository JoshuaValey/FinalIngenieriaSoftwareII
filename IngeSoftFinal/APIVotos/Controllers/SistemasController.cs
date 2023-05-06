using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVotos.Models;

namespace APIVotos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemasController : ControllerBase
    {
        private readonly VotosContext _context;

        public SistemasController(VotosContext context)
        {
            _context = context;
        }

        // GET: api/Sistemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sistema>>> GetSistemas()
        {
          if (_context.Sistemas == null)
          {
              return NotFound();
          }
            return await _context.Sistemas.ToListAsync();
        }

        // GET: api/Sistemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sistema>> GetSistema(int id)
        {
          if (_context.Sistemas == null)
          {
              return NotFound();
          }
            var sistema = await _context.Sistemas.FindAsync(id);

            if (sistema == null)
            {
                return NotFound();
            }

            return sistema;
        }

        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistema(int id, Sistema sistema)
        {
            if (id != sistema.Id)
            {
                return BadRequest();
            }

            _context.Entry(sistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemaExists(id))
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

        // POST: api/Sistemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sistema>> PostSistema(Sistema sistema)
        {
          if (_context.Sistemas == null)
          {
              return Problem("Entity set 'VotosContext.Sistemas'  is null.");
          }
            _context.Sistemas.Add(sistema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSistema", new { id = sistema.Id }, sistema);
        }

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistema(int id)
        {
            if (_context.Sistemas == null)
            {
                return NotFound();
            }
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }

            _context.Sistemas.Remove(sistema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SistemaExists(int id)
        {
            return (_context.Sistemas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
