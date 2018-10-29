using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImSoPunny.Models;

namespace ImSoPunny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunsController : ControllerBase
    {
        private readonly ImSoPunnyContext _context;

        public PunsController(ImSoPunnyContext context)
        {
            _context = context;
        }

        // GET: api/Puns
        [HttpGet]
        public IEnumerable<Pun> GetPun()
        {
            return _context.Puns;
        }

        // GET: api/Puns/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPun([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pun = await _context.Puns.FindAsync(id);

            if (pun == null)
            {
                return NotFound();
            }

            return Ok(pun);
        }

        // PUT: api/Puns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPun([FromRoute] int id, [FromBody] Pun pun)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pun.PunId)
            {
                return BadRequest();
            }

            _context.Entry(pun).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunExists(id))
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

        // POST: api/Puns
        [HttpPost]
        public async Task<IActionResult> PostPun([FromBody] Pun pun)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Puns.Add(pun);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPun", new { id = pun.PunId }, pun);
        }

        // DELETE: api/Puns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePun([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pun = await _context.Puns.FindAsync(id);
            if (pun == null)
            {
                return NotFound();
            }

            _context.Puns.Remove(pun);
            await _context.SaveChangesAsync();

            return Ok(pun);
        }

        private bool PunExists(int id)
        {
            return _context.Puns.Any(e => e.PunId == id);
        }
    }
}