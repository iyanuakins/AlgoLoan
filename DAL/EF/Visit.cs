namespace DAL.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Visit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string userId { get; set; }

        public int providerId { get; set; }

        [Required]
        [StringLength(50)]
        public string providerName { get; set; }

        public DateTime date { get; set; }
    }
}
