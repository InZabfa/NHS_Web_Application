namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medication()
        {
            Patient_Medications = new HashSet<Patient_Medications>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ProviderID { get; set; }

        public DateTime Date_Added { get; set; }

        public int Added_By_StaffID { get; set; }

        public int Max_Dosage_Per_Day { get; set; }

        public int Max_Dosage_Per_Week { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Provider_Info Provider_Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient_Medications> Patient_Medications { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
