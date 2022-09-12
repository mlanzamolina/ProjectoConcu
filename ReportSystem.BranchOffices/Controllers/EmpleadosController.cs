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
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadosContext _context;

        public EmpleadosController(EmpleadosContext context)
        {
            _context = context;
        }

        // GET: api/Empleados or api/empleados?username=Merill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetAsync([FromQuery] string? username)
        {
            if (username == null)
            {
                return await _context.Empleados.ToListAsync();
            }
            return Ok(_context.Empleados.Where(empleados => empleados.username == username));
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDto>> GetEmpleadoDto(string id)
        {
            var empleadoDto = await _context.Empleados.FindAsync(id);

            if (empleadoDto == null)
            {
                return NotFound();
            }

            return empleadoDto;
        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleadoDto(string id, EmpleadoDto empleadoDto)
        {
            if (id != empleadoDto.idEmpleados)
            {
                return BadRequest();
            }

            _context.Entry(empleadoDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoDtoExists(id))
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

        // POST: api/Empleados
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> PostEmpleadoDto(EmpleadoDto empleadoDto)
        {
            _context.Empleados.Add(empleadoDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpleadoDtoExists(empleadoDto.idEmpleados))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmpleadoDto", new { id = empleadoDto.idEmpleados }, empleadoDto);
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpleadoDto>> DeleteEmpleadoDto(string id)
        {
            var empleadoDto = await _context.Empleados.FindAsync(id);
            if (empleadoDto == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleadoDto);
            await _context.SaveChangesAsync();

            return empleadoDto;
        }

        private bool EmpleadoDtoExists(string id)
        {
            return _context.Empleados.Any(e => e.idEmpleados == id);
        }
    }
}
