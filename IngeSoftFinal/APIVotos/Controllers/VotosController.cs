using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIVotos.Models;
using NuGet.Versioning;

namespace APIVotos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly VotosContext _context;

        public VotosController(VotosContext context)
        {
            _context = context;
        }




        [HttpPost]
        public async Task<ActionResult<Voto>> PostVoto(Voto voto)
        {
            if (_context.Votos == null)
            {
                return Problem("Entity set 'VotosContext.Votos'  is null.");
            }

            if (VotoExists(voto.VotanteDpi))
            {

                // Logica para estadisticas de fraude
                var est = await _context.Estadisticas.FindAsync(0);

                est.Fraudes++;

                await _context.SaveChangesAsync();

                return BadRequest("Error 404 not found, intendo de fraude");
            }
            else
            {
                _context.Votos.Add(voto);
                try
                {
                    // Logica para estadisticas votos
                    var est = await _context.Estadisticas.FindAsync(0);

                    est.Votos++;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (VotoExists(voto.VotanteDpi))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            } 

            return CreatedAtAction("GetVoto", new { id = voto.VotanteDpi }, voto);
        }


        private bool VotoExists(string votanteDpi)
        {
            return (_context.Votos?.Any(e => e.VotanteDpi == votanteDpi)).GetValueOrDefault();
        }
    }
}
