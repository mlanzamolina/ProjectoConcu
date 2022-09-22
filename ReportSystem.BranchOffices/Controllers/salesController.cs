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
    public class salesController : ControllerBase
    {
        private readonly salesContext _context;

        public salesController(salesContext context)
        {
            _context = context;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<salesDto>>> Getsales()
        {
            return await _context.sales.ToListAsync();
        }


        // GET: api/sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<salesDto>> GetsalesDto(string id)
        {
            var salesDto = await _context.sales.FindAsync(id);

            if (salesDto == null)
            {
                return NotFound();
            }

            return salesDto;
        }

        // PUT: api/sales/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutsalesDto(string id, salesDto salesDto)
        {
            if (id != salesDto.username)
            {
                return BadRequest();
            }

            _context.Entry(salesDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!salesDtoExists(id))
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

        // POST: api/sales
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<salesDto>> PostsalesDto(salesDto salesDto)
        {
            _context.sales.Add(salesDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (salesDtoExists(salesDto.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetsalesDto", new { id = salesDto.username }, salesDto);
        }

        // DELETE: api/sales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<salesDto>> DeletesalesDto(string id)
        {
            var salesDto = await _context.sales.FindAsync(id);
            if (salesDto == null)
            {
                return NotFound();
            }

            _context.sales.Remove(salesDto);
            await _context.SaveChangesAsync();

            return salesDto;
        }

        private bool salesDtoExists(string id)
        {
            return _context.sales.Any(e => e.username == id);
        }
    }
}
