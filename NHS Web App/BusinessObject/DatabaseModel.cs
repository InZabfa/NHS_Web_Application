namespace BusinessObject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseModel : DbContext
    {
        public DatabaseModel()
            : base("name=DatabaseModel")
        {
        }

        public virtual DbSet<Access_Levels> Access_Levels { get; set; }
        public virtual DbSet<Access_Types> Access_Types { get; set; }
        public virtual DbSet<Appointment_Completion> Appointment_Completion { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Emergency_Contacts> Emergency_Contacts { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Patient_Conditions> Patient_Conditions { get; set; }
        public virtual DbSet<Patient_Notes> Patient_Notes { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Practice_Info> Practice_Info { get; set; }
        public virtual DbSet<Provider_Info> Provider_Info { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Patient_Medications> Patient_Medications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOptional(e => e.Appointment_Completion)
                .WithRequired(e => e.Appointment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Medication>()
                .HasOptional(e => e.Stock)
                .WithRequired(e => e.Medication)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Practice_Info>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Practice_Info)
                .HasForeignKey(e => e.PracticeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Provider_Info>()
                .HasMany(e => e.Medications)
                .WithRequired(e => e.Provider_Info)
                .HasForeignKey(e => e.ProviderID);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Appointment_Completion)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Conditions)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Added_By_StaffID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Medications)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.Added_By_StaffID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Patient_Conditions)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Patient_Notes)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Patient_Medications)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Access_Levels)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AssociatedUserID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Patient_Conditions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PatientID);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Patient)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Patient_Medications)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PatientID);
        }
    }
}
