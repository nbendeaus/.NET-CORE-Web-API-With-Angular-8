using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngApi.DAL.Model;
using AngApi.Models;
using AngApi.DAL.ViewModel;

namespace AngApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public StaffController(AuthenticationContext context)
        {
            _context = context;
        }
        //public IEnumerable<Staff> GetStaffs()
        //{
        //    return _context.Staffs;
        //}
        // GET: api/Staff
        // [HttpGet]
        // No need StaffViewModel to display LINQ JOIN query result, use System.Object
        /*   public IEnumerable<StaffViewModel> GetStaff()
           {
               var empDetails = (from staff in _context.Staffs
                                 join unit in _context.Units on staff.UnitId equals unit.Id
                                 join department in _context.Departments on staff.DepartmentId equals department.Id

                                 select new StaffViewModel
                                 {
                                     Id = staff.Id,
                                     UnitId = unit.Id,
                                     DepartmentId = department.Id,
                                     Name = staff.Name,
                                     UnitName = unit.Name,
                                     DepartmentName = department.Name
                                 }).ToList();
                return empDetails;
           } */

        // GET: api/Staff
        [HttpGet]
        public System.Object GetStaff()
        {
            //return _context.Staffs;
            var empDetails = (from staff in _context.Staffs
                              join unit in _context.Units on staff.UnitId equals unit.Id
                              join department in _context.Departments on staff.DepartmentId equals department.Id

                              select new 
                              {
                                  Id = staff.Id,
                                  UnitId = unit.Id,
                                  DepartmentId = department.Id,
                                  Name = staff.Name,
                                  UnitName = unit.Name,
                                  DepartmentName = department.Name
                              }).ToList();
            return empDetails;
        }


        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = await _context.Staffs.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff([FromRoute] int id, [FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.Id)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // POST: api/Staff
        [HttpPost]
        public async Task<IActionResult> PostStaff([FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return Ok(staff);
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.Id == id);
        }
    }
}