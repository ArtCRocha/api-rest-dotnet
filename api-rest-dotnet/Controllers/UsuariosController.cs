using api_rest_dotnet.Migrations;
using api_rest_dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDto model)
        {
            Usuario novoUsuario = new Usuario { 
             Nome = model.Nome,
             Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),
            Pefil = model.Pefil
        };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = novoUsuario.Id }, novoUsuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDto model)
        {
            if (id != model.Id) return BadRequest();
            var modelDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (modelDb == null) return NotFound();

            modelDb.Nome = model.Nome;
            modelDb.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);
            modelDb.Pefil = model.Pefil;    

            _context.Usuarios.Update(modelDb);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Usuarios.FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
