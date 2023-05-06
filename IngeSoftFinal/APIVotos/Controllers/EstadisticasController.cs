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
    public class EstadisticasController : ControllerBase
    {
        private readonly VotosContext _context;

        public EstadisticasController(VotosContext context)
        {
            _context = context;
        }

        // GET: api/Estadisticas
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Estadistica>>> GetEstadisticas()
        //{
        //  if (_context.Estadisticas == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Estadisticas.ToListAsync();
        //}

        // GET: api/Estadisticas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estadistica>> GetEstadistica(int id)
        {
            if (_context.Estadisticas == null)
            {
                return NotFound();
            }
            var estadistica = await _context.Estadisticas.FindAsync(id);

            if (estadistica == null)
            {
                return NotFound();
            }

            return estadistica;
        }

        //// PUT: api/Estadisticas/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEstadistica(int id, Estadistica estadistica)
        //{
        //    if (id != estadistica.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(estadistica).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EstadisticaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Estadisticas
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Estadistica>> PostEstadistica(Estadistica estadistica)
        //{
        //  if (_context.Estadisticas == null)
        //  {
        //      return Problem("Entity set 'VotosContext.Estadisticas'  is null.");
        //  }
        //    _context.Estadisticas.Add(estadistica);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEstadistica", new { id = estadistica.Id }, estadistica);
        //}

        //// DELETE: api/Estadisticas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEstadistica(int id)
        //{
        //    if (_context.Estadisticas == null)
        //    {
        //        return NotFound();
        //    }
        //    var estadistica = await _context.Estadisticas.FindAsync(id);
        //    if (estadistica == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Estadisticas.Remove(estadistica);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EstadisticaExists(int id)
        //{
        //    return (_context.Estadisticas?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
