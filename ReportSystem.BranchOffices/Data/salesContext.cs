using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportSystem.BranchOffices.Models;

namespace ReportSystem.BranchOffices.Data
{
    public class salesContext : DbContext
    {
        public salesContext(DbContextOptions<salesContext> options)
           : base(options)
        {
        }

        public DbSet<salesDto> sales { get; set; }
    }
}
