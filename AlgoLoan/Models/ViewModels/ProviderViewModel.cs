using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoLoan.Models
{
    public class ProviderViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal minRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxRate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal minAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal maxAmount { get; set; }

        public int minDuration { get; set; }

        public int maxDuration { get; set; }

        [Required]
        [StringLength(200)]
        public string link { get; set; }

        public bool studentLoan { get; set; }

        public bool individualLoan { get; set; }

        public bool businessLoan { get; set; }

        public int uniqueClicks { get; set; }

        public DateTime dateAdded { get; set; }

        public int totalClicks { get; set; }

        public DateTime? lastVisited { get; set; }
    }
}