using System;
using System.ComponentModel.DataAnnotations;

namespace ReportSystem.BranchOffices.Models
{
    public class branchofficesDto
    {
        [Key]
        public string idBranchOffices { get; set; }

        public string state { get; set; }
        public string country { get; set; }
    }
}
