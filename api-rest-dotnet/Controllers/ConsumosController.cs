﻿using api_rest_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_rest_dotnet.Controllers
{
    [Route("api/consumos")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Consumos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Consumo model)
        {
            _context.Consumos.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Consumos.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, Consumo model)
        {
            if (id != model.Id) return BadRequest();
            var modelDb = await _context.Consumos.FirstOrDefaultAsync(x => x.Id == id);

            if (modelDb == null) return NotFound();

            _context.Consumos.Update(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Consumos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Consumos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
