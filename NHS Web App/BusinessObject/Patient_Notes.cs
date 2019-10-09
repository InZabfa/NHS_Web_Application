namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient_Notes
    {
        public int Id { get; set; }

        public int StaffID { get; set; }

        public DateTime Added_DateTime { get; set; }

        public int Lowest_Access_Level_Required { get; set; }

        public int UserID { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual User User { get; set; }
    }
}
