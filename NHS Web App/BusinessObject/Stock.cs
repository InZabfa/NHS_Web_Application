namespace BusinessObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedicineID { get; set; }

        public int Quantity { get; set; }

        public virtual Medication Medication { get; set; }
    }
}
