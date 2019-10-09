namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Emergency_Contacts = new HashSet<Emergency_Contacts>();
            Logs = new HashSet<Log>();
            Patient_Conditions = new HashSet<Patient_Conditions>();
            Patient_Notes = new HashSet<Patient_Notes>();
            Staffs = new HashSet<Staff>();
            Patient_Medications = new HashSet<Patient_Medications>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Forename { get; set; }

        [Required]
        [StringLength(70)]
        public string Surname { get; set; }

        public bool Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(175)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string Phone_number { get; set; }

        public int PracticeID { get; set; }

        public virtual Access_Levels Access_Levels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emergency_Contacts> Emergency_Contacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Conditions> Patient_Conditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Notes> Patient_Notes { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Practice_Info Practice_Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff> Staffs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Medications> Patient_Medications { get; set; }
    }
}
