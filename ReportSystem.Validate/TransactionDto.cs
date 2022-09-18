using System;
using System.ComponentModel.DataAnnotations;

namespace ReportSystem.Gateway.Models
{
    public class TransactionDto
    {
        [Key]
        public string transactionId { get; set; }

        public string errors { get; set; }
        public string fecha { get; set; }
    }
}
