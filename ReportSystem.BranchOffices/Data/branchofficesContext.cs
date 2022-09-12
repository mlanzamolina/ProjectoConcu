using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportSystem.BranchOffices.Models;

namespace ReportSystem.BranchOffices.Data
{
    public class branchofficesContext : DbContext
    {
        public branchofficesContext(DbContextOptions<branchofficesContext> options)
           : base(options)
        {
        }

        public DbSet<branchofficesDto> branchOffices { get; set; }
    }
}
