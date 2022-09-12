using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportSystem.BranchOffices.Models;

namespace ReportSystem.BranchOffices.Data
{
    public class EmpleadosContext : DbContext
    {
        public EmpleadosContext(DbContextOptions<EmpleadosContext> options)
           : base(options)
        {
        }

        public DbSet<EmpleadoDto> Empleados { get; set; }
    }
}
