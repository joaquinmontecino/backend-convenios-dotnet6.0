using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCUCMAPI.Data;
using SGCUCMAPI.Models;

namespace SGCUCMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadGestoraController : ControllerBase
    {
        private readonly DataContext _context;
        public UnidadGestoraController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<List<UnidadGestora>>> GetUnidadesGestoras(int? id = null)
        {
            if (id.HasValue)
            {
                var unidad = await _context.UnidadesGestoras.FindAsync(id.Value);
                if (unidad == null)
                {
                    return NotFound();
                }
                return Ok(unidad);
            }
            else
            {
                var unidades = await _context.UnidadesGestoras.ToListAsync();
                return Ok(unidades);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<UnidadGestora>>> CreateUnidadGestora(UnidadGestora unidad)
        {
            _context.UnidadesGestoras.Add(unidad);
            await _context.SaveChangesAsync();

            return Ok(unidad);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<UnidadGestora>>> UpdateUnidadGestora(int id, UnidadGestora unidad)
        {
            var dbUnidad = await _context.UnidadesGestoras.FindAsync(id);
            if (dbUnidad == null)
            {
                return BadRequest("Unidad Gestora no encontrada");
            }

            dbUnidad.IdInstitucion = unidad.IdInstitucion;
            dbUnidad.NombreUnidad = unidad.NombreUnidad;

            await _context.SaveChangesAsync();

            return Ok(dbUnidad);
        }

        [HttpDelete]
        public async Task<ActionResult<List<UnidadGestora>>> DeleteUnidadGestora(int id)
        {
            var dbUnidad = await _context.UnidadesGestoras.FindAsync(id);
            if (dbUnidad == null)
            {
                return BadRequest("Unidad Gestora no encontrada");
            }

            _context.UnidadesGestoras.Remove(dbUnidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
