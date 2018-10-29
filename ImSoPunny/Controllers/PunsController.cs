using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImSoPunny.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImSoPunny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunsController : ControllerBase
    {
        private readonly ImSoPunnyContext _db;

        public PunsController(ImSoPunnyContext context)
        {
            _db = context;
        }

        // api/puns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PunDtoReturn>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try {
                var Pun = await _db.Puns
                .Where(p => p.PunId == id)
                .Include(pt => pt.PunTags)
                .ThenInclude(tags => tags.Tag)
                .Select(p => new PunDtoReturn
                {
                    PunId = p.PunId,
                    Text = p.Text,
                    Score = p.Score,
                    User = p.User,
                    Tags = p.PunTags.Select(t => t.Tag.Text)
                }).FirstAsync();
                if (Pun == null)
                {
                    return NotFound();
                }
                return Ok(Pun);
            } catch( Exception e) {
                return BadRequest(e);
            }

        }

        // api/puns/sample
        [HttpGet("sample")]
        public async Task<ActionResult<PunDtoReturn>> Sample() {
            var count = await _db.Puns.CountAsync();
            Random rnd = new Random();
            var id = rnd.Next(1, count);
            return await Get(id);
        }

        // api/puns/filter/term
        [HttpGet("filter/{term}")]
        public async Task<ActionResult<List<Pun>>> Filter(string term) {
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            var Puns = await _db.Puns
                .Where(x => x.PunTags.Any(t => t.Tag.Acronym == term))
                .Include(pt => pt.PunTags)
                .ThenInclude(tags => tags.Tag)
                .Select(p => new PunDtoReturn
                {
                    PunId = p.PunId,
                    Text = p.Text,
                    Score = p.Score,
                    User = p.User,
                    Tags = p.PunTags.Select(t => t.Tag.Text)
                })
                .ToListAsync();
            if(Puns == null) {
                return NoContent(); // No items found by term
            }
            return Ok(Puns);
        }
    }
}
