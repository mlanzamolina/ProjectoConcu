using System;
using System.ComponentModel.DataAnnotations;

namespace ReportSystem.BranchOffices.Models
{
    public class carsDto
    {
        [Key]
        public string idCars { get; set; }

        public string name { get; set; }
        public string VIN { get; set; }
        public string branchOfficeId { get; set; }
    }
}
