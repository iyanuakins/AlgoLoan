using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoLoan.Models
{
    public class ProviderViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string name { get; set; }
        [Display(Name = "Minimum Rate")]
        [Required]
        [Column(TypeName = "numeric")]
        public decimal minRate { get; set; }

        [Display(Name = "Maximum Rate")]
        [Column(TypeName = "numeric")]
        public decimal maxRate { get; set; }

        [Display(Name = "Minimum Amount")]
        [Required]
        [Column(TypeName = "numeric")]
        public decimal minAmount { get; set; }

        [Display(Name = "Maximum Amount")]
        [Required]
        [Column(TypeName = "numeric")]
        public decimal maxAmount { get; set; }

        [Display(Name = "Minimum Duration")]
        [Required]
        public int minDuration { get; set; }

        [Display(Name = "Maximum Duration")]
        [Required]
        public int maxDuration { get; set; }

        [Display(Name = "Link")]
        [Required]
        [StringLength(200)]
        public string link { get; set; }

        [Display(Name = "Student Loan" , Description = "Does Provider offer student loan?")]
        [Required]
        public bool studentLoan { get; set; }


        [Display(Name = "Individual Loan", Description = "Does Provider offer individual loan?")]
        [Required]
        public bool individualLoan { get; set; }


        [Display(Name = "Business Loan", Description = "Does Provider offer business loan?")]
        [Required]
        public bool businessLoan { get; set; }

        public int uniqueClicks { get; set; }

        public DateTime dateAdded { get; set; }

        public int totalClicks { get; set; }

        public DateTime? lastVisited { get; set; }
    }
}