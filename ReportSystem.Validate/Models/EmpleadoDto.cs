using System;
using System.ComponentModel.DataAnnotations;

namespace ReportSystem.BranchOffices.Models
{
    public class EmpleadoDto
    {
        [Key]
        public string idEmpleados { get; set; }

        public string name { get; set; }
        public string lastname { get; set; }

        public string username { get; set; }
        public string branchOfficeId { get; set; }
    }
}
