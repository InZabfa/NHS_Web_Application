namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient_Medications
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PatientID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicationID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Date_Time { get; set; }

        public string Note { get; set; }

        public int? Dosage_Per_Day { get; set; }

        public int? Dosage_Per_Week { get; set; }

        public virtual Medication Medication { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual User User { get; set; }
    }
}
