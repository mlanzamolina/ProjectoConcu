using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportSystem.BranchOffices.Models;

namespace ReportSystem.BranchOffices.Data
{
    public class carsContext : DbContext
    {
        public carsContext(DbContextOptions<carsContext> options)
           : base(options)
        {
        }

        public DbSet<carsDto> Cars { get; set; }
    }
}
