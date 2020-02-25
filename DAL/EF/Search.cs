namespace DAL.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Search
    {
        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal amount { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        public int duration { get; set; }
    }
}
