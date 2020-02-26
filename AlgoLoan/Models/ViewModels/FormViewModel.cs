using System.ComponentModel.DataAnnotations;

namespace AlgoLoan.Models
{
    public class FormViewModel
    {
        [Required]
        [Range(1, 6000000, ErrorMessage = "Enter a valid amount between 1 to 6,000,000")]
        public int Amount { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Type { get; set; }
    }
}