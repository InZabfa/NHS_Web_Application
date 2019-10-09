namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public int Id { get; set; }

        public int AssociatedUserID { get; set; }

        public DateTime LoggedDate_Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
