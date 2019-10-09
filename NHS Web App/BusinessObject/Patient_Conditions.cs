namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient_Conditions
    {
        public int Id { get; set; }

        public int PatientID { get; set; }

        public int ConditionID { get; set; }

        public int StaffID { get; set; }

        public DateTime Date_Time { get; set; }

        public string Note { get; set; }

        public virtual Condition Condition { get; set; }

        public virtual User User { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
