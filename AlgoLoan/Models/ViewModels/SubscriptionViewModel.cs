using System;
using System.ComponentModel.DataAnnotations;

namespace AlgoLoan.Models
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string userId { get; set; }

        [Required]
        [StringLength(20)]
        public string type { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public DateTime lastSubDate { get; set; }
    }
}