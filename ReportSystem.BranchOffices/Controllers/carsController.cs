using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportSystem.BranchOffices.Data;
using ReportSystem.BranchOffices.Models;

namespace ReportSystem.BranchOffices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carsController : ControllerBase
    {
        private readonly carsContext _context;

        public carsController(carsContext context)
        {
            _context = context;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<carsDto>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<carsDto>> GetcarsDto(string id)
        {
            var carsDto = await _context.Cars.FindAsync(id);

            if (carsDto == null)
            {
                return NotFound();
            }

            return carsDto;
        }

        // PUT: api/cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutcarsDto(string id, carsDto carsDto)
        {
            if (id != carsDto.idCars)
            {
                return BadRequest();
            }

            _context.Entry(carsDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!carsDtoExists(id))
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

        // POST: api/cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<carsDto>> PostcarsDto(carsDto carsDto)
        {
            _context.Cars.Add(carsDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (carsDtoExists(carsDto.idCars))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetcarsDto", new { id = carsDto.idCars }, carsDto);
        }

        // DELETE: api/cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<carsDto>> DeletecarsDto(string id)
        {
            var carsDto = await _context.Cars.FindAsync(id);
            if (carsDto == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(carsDto);
            await _context.SaveChangesAsync();

            return carsDto;
        }

        private bool carsDtoExists(string id)
        {
            return _context.Cars.Any(e => e.idCars == id);
        }
    }
}
