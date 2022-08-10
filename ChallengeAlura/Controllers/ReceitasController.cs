using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChallengeAlura.Data;
using ChallengeAlura.Models;

namespace ChallengeAlura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly ChallengeAluraContext _context;

        public ReceitasController(ChallengeAluraContext context)
        {
            _context = context;
        }

        // GET: api/Receitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receitas>>> GetReceitas()
        {
            if (_context.Receitas == null)
            {
                return NotFound();
            }
            return await _context.Receitas.ToListAsync();
        }

        // GET: api/Receitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receitas>> GetReceitas(Guid id)
        {
            if (_context.Receitas == null)
            {
                return NotFound();
            }
            var receitas = await _context.Receitas.FindAsync(id);

            if (receitas == null)
            {
                return NotFound();
            }

            return receitas;
        }

        // PUT: api/Receitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceitas(Guid id, Receitas receitas)
        {
            if (id != receitas.Id)
            {
                return BadRequest();
            }

            if (ReceitaDuplicadaNoMes(receitas))
            {
                return Problem("ATENÇÃO: Receita duplicada no mesmo mês!");
            }

            _context.Entry(receitas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceitasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Receitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receitas>> PostReceitas(Receitas receitas)
        {
            if (_context.Receitas == null)
            {
                return Problem("Entity set 'ChallengeAluraContext.Receitas'  is null.");
            }

            if (ReceitaDuplicadaNoMes(receitas))
            {
                return Problem("ATENÇÃO: Receita duplicada no mesmo mês!");
            }

            _context.Receitas.Add(receitas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceitas", new { id = receitas.Id }, receitas);
        }

        // DELETE: api/Receitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceitas(Guid id)
        {
            if (_context.Receitas == null)
            {
                return NotFound();
            }
            var receitas = await _context.Receitas.FindAsync(id);
            if (receitas == null)
            {
                return NotFound();
            }

            _context.Receitas.Remove(receitas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceitasExists(Guid id)
        {
            return (_context.Receitas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool ReceitaDuplicadaNoMes(Receitas receitas)
        {
            return (_context.Receitas?.Any(e => e.Id != receitas.Id &&
                                                e.Descricao == receitas.Descricao &&
                                                e.Data.Month == receitas.Data.Month &&
                                                e.Data.Year == receitas.Data.Year)).GetValueOrDefault();
        }
    }
}
