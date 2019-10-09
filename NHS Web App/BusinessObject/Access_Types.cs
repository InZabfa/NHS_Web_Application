namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Access_Types
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccessLevel { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
