using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IngeSoftFinal.Models;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IngeSoftFinal.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly VotosContext _context;
        private string URL = "http://localhost:5129/api/Sistemas"; // Cambiar Ruta
        private readonly HttpClient _httpClient;
        public CandidatosController(VotosContext context)
        {
            _context = context;
            _httpClient = new HttpClient();

           
        }

        public async Task<bool> Closed()
        {
            var httpResponse = await _httpClient.GetAsync($"{URL}/{1}");

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                var sis = JsonConvert.DeserializeObject<Sistema>(result);

                if ((bool)sis.Vigente)
                {
                    return false;
                }
                else
                {
                    return true;
                }



            }
            else return false;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }


            return _context.Candidatos != null ? 
                          View(await _context.Candidatos.ToListAsync()) :
                          Problem("Entity set 'VotosContext.Candidatos'  is null.");
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // GET: Candidatos/Create
        public  async Task< IActionResult >Create()
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }


            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Partido")] Candidato candidato)
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (ModelState.IsValid)
            {
                _context.Add(candidato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidato);
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }
            return View(candidato);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Partido")] Candidato candidato)
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (id != candidato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoExists(candidato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidato);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Closed())
            {
                return StatusCode(400, "Bad Request **** El proceso de Candidatos ha terminado");
            }
            if (_context.Candidatos == null)
            {
                return Problem("Entity set 'VotosContext.Candidatos'  is null.");
            }
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato != null)
            {
                _context.Candidatos.Remove(candidato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoExists(int id)
        {
          return (_context.Candidatos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
