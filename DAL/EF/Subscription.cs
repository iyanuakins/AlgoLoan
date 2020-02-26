namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subscription
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

        public virtual User User { get; set; }
    }
}
