using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngApi.DAL.Model;
using AngApi.Models;

namespace AngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PutUpController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public PutUpController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/PutUp
        [HttpGet]
        public IEnumerable<PutUp> GetPutUps()
        {
            return _context.PutUps;
        }

        // GET: api/PutUp/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPutUp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var putUp = await _context.PutUps.FindAsync(id);

            if (putUp == null)
            {
                return NotFound();
            }

            return Ok(putUp);
        }

        // PUT: api/PutUp/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPutUp([FromRoute] int id, [FromBody] PutUp putUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != putUp.Id)
            {
                return BadRequest();
            }

            _context.Entry(putUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PutUpExists(id))
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

        // POST: api/PutUp
        [HttpPost]
        public async Task<IActionResult> PostPutUp([FromBody] PutUp putUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PutUps.Add(putUp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPutUp", new { id = putUp.Id }, putUp);
        }

        // DELETE: api/PutUp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePutUp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var putUp = await _context.PutUps.FindAsync(id);
            if (putUp == null)
            {
                return NotFound();
            }

            _context.PutUps.Remove(putUp);
            await _context.SaveChangesAsync();

            return Ok(putUp);
        }

        private bool PutUpExists(int id)
        {
            return _context.PutUps.Any(e => e.Id == id);
        }
    }
}