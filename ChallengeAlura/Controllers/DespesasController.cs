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
    public class DespesasController : ControllerBase
    {
        private readonly ChallengeAluraContext _context;

        public DespesasController(ChallengeAluraContext context)
        {
            _context = context;
        }

        // GET: api/Despesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Despesas>>> GetDespesas()
        {
            if (_context.Despesas == null)
            {
                return NotFound();
            }
            return await _context.Despesas.ToListAsync();
        }

        // GET: api/Despesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Despesas>> GetDespesas(Guid id)
        {
            if (_context.Despesas == null)
            {
                return NotFound();
            }
            var despesas = await _context.Despesas.FindAsync(id);

            if (despesas == null)
            {
                return NotFound();
            }

            return despesas;
        }

        // PUT: api/Despesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDespesas(Guid id, Despesas despesas)
        {
            if (id != despesas.Id)
            {
                return BadRequest();
            }

            if (DespesaDuplicadaNoMes(despesas))
            {
                return Problem("ATENÇÃO: Receita duplicada no mesmo mês!");
            }

            if (despesas.Categoria == Despesas.Categorias.None)
                despesas.Categoria = Despesas.Categorias.Outras;

            _context.Entry(despesas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DespesasExists(id))
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

        // POST: api/Despesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Despesas>> PostDespesas(Despesas despesas)
        {
            if (_context.Despesas == null)
            {
                return Problem("Entity set 'ChallengeAluraContext.Despesas'  is null.");
            }

            if (DespesaDuplicadaNoMes(despesas))
            {
                return Problem("ATENÇÃO: Receita duplicada no mesmo mês!");
            }

            if (despesas.Categoria == Despesas.Categorias.None)
                despesas.Categoria = Despesas.Categorias.Outras;

            _context.Despesas.Add(despesas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDespesas", new { id = despesas.Id }, despesas);
        }

        // DELETE: api/Despesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDespesas(Guid id)
        {
            if (_context.Despesas == null)
            {
                return NotFound();
            }
            var despesas = await _context.Despesas.FindAsync(id);
            if (despesas == null)
            {
                return NotFound();
            }

            _context.Despesas.Remove(despesas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DespesasExists(Guid id)
        {
            return (_context.Despesas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool DespesaDuplicadaNoMes(Despesas despesas)
        {
            return (_context.Despesas?.Any(e => e.Id != despesas.Id &&
                                                e.Descricao == despesas.Descricao &&
                                                e.Data.Month == despesas.Data.Month &&
                                                e.Data.Year == despesas.Data.Year)).GetValueOrDefault();
        }
    }
}
