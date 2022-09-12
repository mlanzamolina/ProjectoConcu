using System;
using System.ComponentModel.DataAnnotations;

namespace ReportSystem.BranchOffices.Models
{
    public class salesDto
    {
        [Key]
        public string username { get; set; }

        public string idCars { get; set; }
        public double price { get; set; }
        public string VIN { get; set; }
        public string buyer_name { get; set; }
        public string buyer_last_name { get; set; }
        public string buyer_id { get; set; }
        public string division_id { get; set; }
    }
}

