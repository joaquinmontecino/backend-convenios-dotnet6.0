using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCUCMAPI.Data;
using SGCUCMAPI.Models;

namespace SGCUCMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly DataContext _context;
        public InstitucionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<List<Institucion>>> GetInstituciones(int? id = null)
        {
            if(id.HasValue)
            {
                var institucion = await _context.Instituciones.FindAsync(id.Value);
                if(institucion == null)
                {
                    return NotFound();
                }
                return Ok(institucion);
            }
            else
            {
                var instituciones = await _context.Instituciones.ToListAsync();
                return Ok(instituciones);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Institucion>>> CreateInstitucion(Institucion institucion)
        {
            _context.Instituciones.Add(institucion);
            await _context.SaveChangesAsync();

            return Ok(institucion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Institucion>>> UpdateInstitucion(int id, Institucion institucion)
        {
            var dbInstitucion = await _context.Instituciones.FindAsync(id);
            if (dbInstitucion == null)
            {
                return BadRequest("Institucion no encontrada");
            }

            dbInstitucion.NombreInstitucion = institucion.NombreInstitucion;
            dbInstitucion.Pais = institucion.Pais;
            dbInstitucion.Alcance = institucion.Alcance;
            dbInstitucion.TipoInstitucion = institucion.TipoInstitucion;

            await _context.SaveChangesAsync();

            return Ok(dbInstitucion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Institucion>>> DeleteInstitucion(int id)
        {
            var dbInstitucion = await _context.Instituciones.FindAsync(id);
            if (dbInstitucion == null)
            {
                return BadRequest("Institucion no encontrada");
            }

            _context.Instituciones.Remove(dbInstitucion);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpGet("nombresInstituciones")]
        public async Task<ActionResult<List<Institucion>>> GetInstitucionesSoloNombre()
        {
            var instituciones = await _context.Instituciones
                .Where(i => i.IdInstitucion != 1)
                .OrderBy(i => i.IdInstitucion)
                .Select(i => new { Id_Institucion = i.IdInstitucion, Nombre_Institucion = i.NombreInstitucion })
                .ToListAsync();

            return Ok(instituciones);
        }
    }
}
