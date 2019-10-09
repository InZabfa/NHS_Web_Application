namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Emergency_Contacts
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Forename { get; set; }

        [Required]
        [StringLength(70)]
        public string Surname { get; set; }

        [Required]
        [StringLength(175)]
        public string Address { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone_Number { get; set; }

        [Required]
        [StringLength(20)]
        public string Relation { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
