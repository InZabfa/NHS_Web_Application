namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        public int Id { get; set; }

        public DateTime Appointment_DateTime { get; set; }

        public int Appointment_Duration_Minutes { get; set; }

        public int StaffID { get; set; }

        [Required]
        [StringLength(10)]
        public string Ref_Number { get; set; }

        [Required]
        [StringLength(10)]
        public string Room { get; set; }

        public int PatientUserID { get; set; }

        public virtual Appointment_Completion Appointment_Completion { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
