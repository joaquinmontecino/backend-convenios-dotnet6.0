using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCUCMAPI.Data;
using SGCUCMAPI.Models;

namespace SGCUCMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios (int? id = null)
        {
            if (id.HasValue)
            {
                var dbUsuario = await _context.Usuarios.FindAsync(id.Value);
                if (dbUsuario == null)
                {
                    return NotFound();
                }
                var usuario = new
                {
                    email = dbUsuario.Email,
                    nombre = dbUsuario.Nombre,
                    apellido = dbUsuario.Apellido,
                };
                return Ok(usuario);
            }
            else
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return Ok(usuarios);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> RegisterUsuario (Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        /*
        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> LoginUsuario ()
        {
            
        }
        */

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(int id, Usuario usuario)
        {
            var dbUsuario = await _context.Usuarios.FindAsync(id);
            if (dbUsuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            dbUsuario.Email = usuario.Email;
            dbUsuario.Contrasena = usuario.Contrasena;
            dbUsuario.Nombre = usuario.Nombre;
            dbUsuario.Apellido = usuario.Apellido;
            dbUsuario.Vigencia = usuario.Vigencia;
            dbUsuario.Privilegios = usuario.Privilegios;

            await _context.SaveChangesAsync();

            return Ok(dbUsuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario (int id)
        {
            var dbUsuario = await _context.Usuarios.FindAsync(id);
            if (dbUsuario == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            _context.Usuarios.Remove(dbUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
