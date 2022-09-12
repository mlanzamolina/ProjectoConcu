/*using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportSystem.BranchOffices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystem.BranchOffices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
       
        private static readonly List<EmpleadoDto> empleados = new List<EmpleadoDto> 
        {
            new EmpleadoDto
            {
                EmpleadoID = Guid.NewGuid(),
                EmpleadoName = "Marco",
                EmpleadoUsername = "mlanz",
                BranchOfficeId = Guid.NewGuid(),
            },
            new EmpleadoDto
            {
                EmpleadoID = Guid.NewGuid(),
                EmpleadoName = "Mario",
                EmpleadoUsername = "mvel",
                BranchOfficeId = Guid.NewGuid(),
            },
             new EmpleadoDto
            {
                EmpleadoID = Guid.NewGuid(),
                EmpleadoName = "Rodrigo",
                EmpleadoUsername = "rbar",
                BranchOfficeId = Guid.NewGuid(),
            }
        };

        public EmpleadoController()
        {
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmpleadoDto>> Get([FromQuery] Guid? EmpleadoID)
        {
            if(EmpleadoID == null)
            {

                return Ok(empleados);
            }
            return Ok(empleados.Where(empleados => empleados.EmpleadoID == EmpleadoID));    
        }
    }
}*/
