namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Access_Levels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public int Access_Level { get; set; }

        public bool Access_Enabled { get; set; }

        public int? Disabled_By_StaffID { get; set; }

        public virtual User User { get; set; }
    }
}
