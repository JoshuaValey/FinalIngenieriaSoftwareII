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
    public class CandidatoAdmController : ControllerBase
    {
        private readonly VotosContext _context;

        public CandidatoAdmController(VotosContext context)
        {
            _context = context;
        }



        // GET: api/CandidatoAdm/5
        [HttpGet("{bit}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int bit)
        {


            switch (bit)
            {
                case 0:

                    try
                    {
                        // Obtener el sistema existente
                        var sistema = await _context.Sistemas.FindAsync(1); // Crear Candidatos

                        if (sistema == null) return NotFound();


                        // Actualizar el estado de vigencia en el sistema
                        sistema.Vigente = false;

                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Error interno del servidor");
                    }

                case 1:
                    try
                    {
                        // Obtener el sistema existente
                        var sistema = await _context.Sistemas.FindAsync(1); // Crear Candidatos

                        if (sistema == null) return NotFound();


                        // Actualizar el estado de vigencia en el sistema
                        sistema.Vigente = true;

                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Error interno del servidor");
                    }
                default:
                    return StatusCode(500, "Error interno del servidor, no Estado valido");
            }






        }


        // POST: api/Candidatos/CerrarCandidatos/1
        //[HttpPost("CerrarCandidatos")]
        //[HttpGet("{bit}")]
        //public async Task<ActionResult> CerrarCandidatos(int bit)
        //{
        //    if (bit != 1)
        //    {
        //        throw new Exception();
        //    }

        //}


    }
}
