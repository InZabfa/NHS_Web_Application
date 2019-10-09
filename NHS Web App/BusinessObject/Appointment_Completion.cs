namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment_Completion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppointmentID { get; set; }

        public int Status { get; set; }

        public DateTime Changed_On { get; set; }

        public int StaffID { get; set; }

        public virtual Appointment Appointment { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
