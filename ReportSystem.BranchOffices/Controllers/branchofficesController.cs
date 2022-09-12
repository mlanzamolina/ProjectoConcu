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
    public class branchofficesController : ControllerBase
    {
        private readonly branchofficesContext _context;

        public branchofficesController(branchofficesContext context)
        {
            _context = context;
        }

        // GET: api/branchoffices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<branchofficesDto>>> GetbranchOffices()
        {
            return await _context.branchOffices.ToListAsync();
        }

        // GET: api/branchoffices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<branchofficesDto>> GetbranchofficesDto(string id)
        {
            var branchofficesDto = await _context.branchOffices.FindAsync(id);

            if (branchofficesDto == null)
            {
                return NotFound();
            }

            return branchofficesDto;
        }

        // PUT: api/branchoffices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutbranchofficesDto(string id, branchofficesDto branchofficesDto)
        {
            if (id != branchofficesDto.idBranchOffices)
            {
                return BadRequest();
            }

            _context.Entry(branchofficesDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!branchofficesDtoExists(id))
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

        // POST: api/branchoffices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<branchofficesDto>> PostbranchofficesDto(branchofficesDto branchofficesDto)
        {
            _context.branchOffices.Add(branchofficesDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (branchofficesDtoExists(branchofficesDto.idBranchOffices))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetbranchofficesDto", new { id = branchofficesDto.idBranchOffices }, branchofficesDto);
        }

        // DELETE: api/branchoffices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<branchofficesDto>> DeletebranchofficesDto(string id)
        {
            var branchofficesDto = await _context.branchOffices.FindAsync(id);
            if (branchofficesDto == null)
            {
                return NotFound();
            }

            _context.branchOffices.Remove(branchofficesDto);
            await _context.SaveChangesAsync();

            return branchofficesDto;
        }

        private bool branchofficesDtoExists(string id)
        {
            return _context.branchOffices.Any(e => e.idBranchOffices == id);
        }
    }
}
