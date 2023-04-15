using api_rest_dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Controllers
{
    [Route("api/veiculos")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() 
        {
            var model = await _context.Viculos.ToListAsync();     
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            if(model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                return BadRequest(new {message = "Ano de fabricação e Ano de Modelo são obrigatórios e devem ser maior que zero."});
            }
            _context.Viculos.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Viculos
                .Include(t => t.Usuarios)
                .ThenInclude(t => t.Usuario)
                .Include(t => t.Consumos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest();
            var modelDb = await _context.Viculos.FirstOrDefaultAsync(x => x.Id == id);

            if (modelDb == null) return NotFound();

            _context.Viculos.Update(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Viculos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Viculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/usuarios")]
        public async Task<ActionResult> AddUsuario(int id, VeiculoUsuarios model)
        {
            if (id != model.VeiculoId) return BadRequest();

            _context.VeiculoUsuarios.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = model.VeiculoId }, model);
        }
        [HttpDelete("{id}/usuarios/{usuarioId}")]
        public async Task<ActionResult> DeleteUsuario(int id, int usuarioId)
        {
            var model = await _context.VeiculoUsuarios
                .Where(c => c.VeiculoId == id && c.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (model == null) return BadRequest();

            _context.VeiculoUsuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
