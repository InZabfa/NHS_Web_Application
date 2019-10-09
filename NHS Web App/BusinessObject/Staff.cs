namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Appointment_Completion = new HashSet<Appointment_Completion>();
            Appointments = new HashSet<Appointment>();
            Conditions = new HashSet<Condition>();
            Medications = new HashSet<Medication>();
            Patient_Conditions = new HashSet<Patient_Conditions>();
            Patient_Notes = new HashSet<Patient_Notes>();
            Patients = new HashSet<Patient>();
            Patient_Medications = new HashSet<Patient_Medications>();
        }

        public int Id { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(30)]
        public string Contract_type { get; set; }

        [Required]
        [StringLength(7)]
        public string Working_days { get; set; }

        public int Working_hours { get; set; }

        [Required]
        [StringLength(30)]
        public string Staff_Role { get; set; }

        public bool FT_PT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment_Completion> Appointment_Completion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Condition> Conditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medication> Medications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Conditions> Patient_Conditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Notes> Patient_Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Medications> Patient_Medications { get; set; }

        public virtual User User { get; set; }
    }
}
