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
    public class MovementDetailController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public MovementDetailController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/MovementDetail
        [HttpGet]
        public System.Object GetMovementDetails()
        {
            // return _context.MovementDetails;
            var movementDetails = (from movementDetail in _context.MovementDetails
                              join putUp in _context.PutUps on movementDetail.PutUpId equals putUp.Id
                              join staff in _context.Staffs on movementDetail.StaffId equals staff.Id

                              select new
                              {
                                  Id = movementDetail.Id,
                                  PutUpId = putUp.Id,
                                  StaffId = staff.Id,
                                  Name = movementDetail.Name,
                                  PutUpSubject = putUp.Subject,
                                  StaffName = staff.Name,
                                  SeqNo = movementDetail.SeqNo,
                                  Comments = movementDetail.Comments,
                                  ReceiveData = movementDetail.ReceiveData,
                                  ActionDate = movementDetail.ActionDate,
                              }).ToList();
            return movementDetails;
        }

        // GET: api/MovementDetail/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovementDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movementDetail = await _context.MovementDetails.FindAsync(id);

            if (movementDetail == null)
            {
                return NotFound();
            }

            return Ok(movementDetail);
        }

        // PUT: api/MovementDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovementDetail([FromRoute] int id, [FromBody] MovementDetail movementDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movementDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(movementDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementDetailExists(id))
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

        // POST: api/MovementDetail
        [HttpPost]
        public async Task<IActionResult> PostMovementDetail([FromBody] MovementDetail movementDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MovementDetails.Add(movementDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovementDetail", new { id = movementDetail.Id }, movementDetail);
        }

        // DELETE: api/MovementDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovementDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movementDetail = await _context.MovementDetails.FindAsync(id);
            if (movementDetail == null)
            {
                return NotFound();
            }

            _context.MovementDetails.Remove(movementDetail);
            await _context.SaveChangesAsync();

            return Ok(movementDetail);
        }

        private bool MovementDetailExists(int id)
        {
            return _context.MovementDetails.Any(e => e.Id == id);
        }
    }
}