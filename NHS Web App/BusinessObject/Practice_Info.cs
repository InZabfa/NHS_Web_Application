namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Practice_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Practice_Info()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }

        [StringLength(175)]
        public string Address { get; set; }

        [StringLength(12)]
        public string Phone_Number { get; set; }

        [StringLength(254)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
