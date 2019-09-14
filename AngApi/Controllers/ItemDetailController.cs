using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngApi.DAL.Model;
using AngApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace AngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ItemDetailController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/ItemDetail
       // [AllowAnonymous]
        [HttpGet]
        public IEnumerable<ItemDetail> GetItemDetails()
        {
            return _context.ItemDetails;

        }
      /*  public async Task<ActionResult<IEnumerable<ItemDetail>>> GetItemDetails()
        {
            return await _context.ItemDetails.ToListAsync();
        } */
        // GET: api/ItemDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemDetail = await _context.ItemDetails.FindAsync(id);

            if (itemDetail == null)
            {
                return NotFound();
            }

            return Ok(itemDetail);
        }

        // PUT: api/ItemDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDetail([FromRoute] int id, [FromBody] ItemDetail itemDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemDetailExists(id))
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

        // POST: api/ItemDetail
        [HttpPost]
        public async Task<IActionResult> PostItemDetail([FromBody] ItemDetail itemDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemDetails.Add(itemDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemDetail", new { id = itemDetail.Id }, itemDetail);
        }

        // DELETE: api/ItemDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemDetail = await _context.ItemDetails.FindAsync(id);
            if (itemDetail == null)
            {
                return NotFound();
            }

            _context.ItemDetails.Remove(itemDetail);
            await _context.SaveChangesAsync();

            return Ok(itemDetail);
        }

        private bool ItemDetailExists(int id)
        {
            return _context.ItemDetails.Any(e => e.Id == id);
        }
    }
}