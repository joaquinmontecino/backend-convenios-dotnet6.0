using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCUCMAPI.Data;
using SGCUCMAPI.Models;

namespace SGCUCMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinadorController : ControllerBase
    {
        private readonly DataContext _context;
        public CoordinadorController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("{id?}")]
        public async Task<ActionResult<List<Coordinador>>> GetCoordinadores(int? id = null)
        {
            if (id.HasValue)
            {
                var coordinador = await _context.Coordinadores.FindAsync(id.Value);
                if (coordinador == null)
                {
                    return NotFound();
                }
                return Ok(coordinador);
            }
            else
            {
                var coordinadores = await _context.Coordinadores.ToListAsync();
                return Ok(coordinadores);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Coordinador>>> CreateCoordinador(Coordinador coordinador)
        {
            _context.Coordinadores.Add(coordinador);
            await _context.SaveChangesAsync();

            return Ok(coordinador);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Coordinador>>> UpdateCoordinador(int id, Coordinador coordinador)
        {
            var dbCoordinador = await _context.Coordinadores.FindAsync(id);
            if (dbCoordinador == null)
            {
                return BadRequest("Coordinador no encontrado");
            }

            dbCoordinador.IdInstitucion = coordinador.IdInstitucion;
            dbCoordinador.Tipo = coordinador.Tipo;
            dbCoordinador.Nombre = coordinador.Nombre;
            dbCoordinador.Correo = coordinador.Correo;

            await _context.SaveChangesAsync();

            return Ok(dbCoordinador);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Coordinador>>> DeleteCoordinador(int id)
        {
            var dbCoordinador = await _context.Coordinadores.FindAsync(id);
            if (dbCoordinador == null)
            {
                return BadRequest("Coordinador no encontrado");
            }

            _context.Coordinadores.Remove(dbCoordinador);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("nombresCoordinadores")]
        public async Task<ActionResult<List<Coordinador>>> GetCoordinadoresSoloNombre()
        {
            var coordinadores = await _context.Coordinadores
                .OrderBy(c => c.IdCoordinador)
                .Select(c => new {Id_Coordinador = c.IdCoordinador, c.Nombre})
                .ToListAsync();

            return Ok(coordinadores);
        }

        [HttpGet("listarCoordinadoresInternos")]
        public async Task<ActionResult<List<Coordinador>>> GetCoordinadoresSoloInternos()
        {
            var coordinadores = await _context.Coordinadores
                .Where(c => c.Tipo == "Interno")
                .OrderBy(c => c.IdCoordinador)
                .Select(c => new { Id_Coordinador = c.IdCoordinador, c.Nombre })
                .ToListAsync();

            return Ok(coordinadores);
        }

    }
}
