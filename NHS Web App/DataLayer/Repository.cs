using BusinessObject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace DataLayer
{
    /// <summary>
    /// Class to handle all manipulation of the database
    /// </summary>
    public class Repository : IDisposable
    {
        #region Don't Touch
        public DatabaseModel DB { get; set; }

        /// <summary>
        /// Repository constructor
        /// </summary>
        public Repository()
        {
            DB = new DatabaseModel();
        }
        /// <summary>
        /// Method to dispose the DatabaseModel object
        /// </summary>
        public void Dispose()
        {
            if (DB != null) DB.Dispose();
        }

        /// <summary>
        /// Method to save changes to the database
        /// </summary>
        public void SaveChanges() => DB.SaveChanges();


        public void SaveChanges<T>(Action<T> action, T args)
        {
            action(args);
            DB.SaveChanges();
        }

        #endregion

        //-------------------------------------------------------------------------------------

        #region Others
        /// <summary>
        /// Returns the access types
        /// </summary>
        /// <returns>All access types as an array</returns>
        public List<Access_Types> AccessTypesGetAll() => DB.Access_Types.ToList();
        public void AccessTypesAdd(Access_Types access) => DB.Access_Types.Add(access);
        public Access_Types AccessTypesGet(int access_level) => DB.Access_Types.Find(access_level);
        public void AccessTypesClear()
        {
            if (DB.Access_Levels.Count() > 0)
                DB.Database.ExecuteSqlCommand("TRUNCATE TABLE [Access_Types]");

        }
        #endregion

        #region User
        /// <summary>
        /// Method to get User by id
        /// </summary>
        /// <param name="id">ID of User</param>
        /// <returns>User with specified id</returns>
        public User UserGet(int id) => DB.Users.FirstOrDefault(u => u.Id == id);
        public List<User> UserGetAll() => DB.Users.ToList();
        public List<User> UserGetAllNonStaff() => DB.Users.OrderBy(u1 => !(u1.Staffs.Count > 0)).ToList();

        /// <summary>
        /// Method to get user via email
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>User with specified email</returns>
        public User UserGet(string email) => DB.Users.FirstOrDefault(u => u.Email == email);

        /// <summary>
        /// Method to add users
        /// </summary>
        /// <param name="user">User account to add</param>
        public void UserAdd(User user) => DB.Users.Add(user);
        #endregion

        #region Patient Notes

        public Patient_Notes GetPatientNote(int id) => DB.Patient_Notes.Find(id);
        public void PatientNoteAdd(Patient_Notes p) => DB.Patient_Notes.Add(p);
        public void PatientNoteRemove(Patient_Notes p) => DB.Patient_Notes.Remove(p);

        #endregion

        #region Patient Conditions
        public void PatientConditionAdd(Patient_Conditions condition) => DB.Patient_Conditions.Add(condition);
        public bool PatientConditionAlreadyHas(Condition condition, int patient_id) => DB.Patient_Conditions.Any(p => p.ConditionID == condition.Id && p.PatientID == patient_id);
        public void PatientConditionRemove(Patient_Conditions condition) => DB.Patient_Conditions.Remove(condition);
        #endregion

        #region Patient Medication
        public void PatientMedicationRemove(Patient_Medications med) => DB.Patient_Medications.Remove(med);
        public void PatientMedicationAdd(Patient_Medications med) => DB.Patient_Medications.Add(med);
        public List<Patient_Medications> PatientMedicationGet(Patient pa) => DB.Patient_Medications.Where(p => p.PatientID == pa.UserID).ToList();
        public List<Patient_Medications> PatientMedicationsGet() => DB.Patient_Medications.ToList();
        #endregion

        #region Log Handling

        /// <summary>
        /// Add a log to the system
        /// Log types:
        /// - LOGIN
        /// - MODIFY_ACCOUNT
        /// - MODIFY_{TABLE NAME}
        /// - ERROR
        /// </summary>
        /// <param name="user">Associated user account</param>
        /// <param name="log_type">Type of logged action/system event</param>
        /// <param name="description">Log description</param>
        public void LogAdd(User user, String log_type, String description = "")
        {
            Log log = new Log()
            {
                User = user,
                LoggedDate_Time = DateTime.Now,
                Type = log_type.ToUpper(),
                Description = description
            };

            DB.Logs.Add(log);
        }
        #endregion

        #region Practice_Info

        /// <summary>
        /// Add practice info to database
        /// </summary>
        /// <param name="id">Practice ID to retrieve</param>
        /// <returns></returns>
        public Practice_Info PracticeGet(int id) => DB.Practice_Info.FirstOrDefault(p => p.Id == id);

        /// <summary>
        /// Add paractice to database
        /// </summary>
        /// <param name="p">Practice to add</param>
        public void PracticeAdd(Practice_Info p) => DB.Practice_Info.Add(p);

        /// <summary>
        /// Returns all practices
        /// </summary>
        /// <returns>a List of type Practice_Info</returns>
        public List<Practice_Info> PracticeGetAll() => DB.Practice_Info.ToList();
        #endregion

        #region Update or modify
        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                DB.Entry(entity).State = EntityState.Modified;
                DB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                DB.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(params object[] entity)
        {
            try
            {
                entity.ToList().ForEach(p => DB.Set(p.GetType()).Remove(p));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveSave(params object[] entity)
        {
            entity.ToList().ForEach(p => Remove(p));
            SaveChanges();
            return true;
        }

        public bool RemoveSave<TEntity>(TEntity entity) where TEntity : class
        {
            var val = Remove(entity);
            SaveChanges();
            return val;
        }
        #endregion

        #region Provider_Info

        /// <summary>
        /// Add provider info to database
        /// </summary>
        /// <param name="p">Provider info to add</param>
        public void ProviderInfoAdd(Provider_Info p) => DB.Provider_Info.Add(p);
        public void ProviderInfoUpdate(int id, Provider_Info new_info) => DB.Entry(DB.Provider_Info.Find(id)).CurrentValues.SetValues(new_info);
        public List<Provider_Info> ProvidersGet() => DB.Provider_Info.ToList();
        public Provider_Info ProviderGet(int id) => DB.Provider_Info.FirstOrDefault(p => p.Id == id);
        public void ProviderRemove(int id) => DB.Provider_Info.Remove(DB.Provider_Info.FirstOrDefault(p => p.Id == id));
        public void ProviderRemove(Provider_Info p) => DB.Provider_Info.Remove(p);

        #endregion

        #region Staff
        /// <summary>
        /// Add staff object to database
        /// </summary>
        /// <param name="s">Staff member to add</param>
        public void StaffAdd(Staff s) => DB.Staffs.Add(s);
        public List<Staff> StaffGetAll() => DB.Staffs.ToList();
        public Boolean UserIsStaff(User user) => DB.Staffs.FirstOrDefault(s => s.UserID == user.Id) != null;
        public Staff StaffGet(User user) => DB.Staffs.FirstOrDefault(s => s.UserID == user.Id);
        public Staff StaffGet(int id) => DB.Staffs.FirstOrDefault(s => s.Id == id);
        #endregion

        #region Stock
        public Stock StockGet(int medID) => DB.Stocks.Find(medID);

        #endregion

        #region Medication

        public void MedicationAdd(Medication m)
        {
            if (m.Stock == null)
                m.Stock = new Stock() { Medication = m, Quantity = 100 };
            DB.Medications.Add(m);
        }

        public void MedicationUpdate(Medication m, Medication newM)
        {
            newM.Id = m.Id;
            newM.ProviderID = m.ProviderID;
            DB.Entry(m).CurrentValues.SetValues(newM);
        }

        public void MedicationStockUpdate(Medication old, int new_quantity)
        {
            Stock stock = DB.Stocks.FirstOrDefault(s => s.MedicineID == old.Id);
            Stock new_Stock = stock;

            new_Stock.Quantity = new_quantity;
            new_Stock.MedicineID = old.Id;

            DB.Entry(stock).CurrentValues.SetValues(stock);
        }
        public Medication MedicationGet(int id) => DB.Medications.FirstOrDefault(m => m.Id == id);
        public List<Medication> MedicationsGet() => DB.Medications.ToList();
        public void MedicationRemove(Medication m) => DB.Medications.Remove(m);
        public void MedicationRemove(int id) => DB.Medications.Remove(DB.Medications.FirstOrDefault(m => m.Id == id));

        #endregion

        #region Conditions
        public List<Condition> ConditionsGet() => DB.Conditions.ToList();
        public Condition ConditionGet(int id) => DB.Conditions.Find(id);
        public void ConditionRemove(Condition item) => DB.Conditions.Remove(item);
        public void ConditionAdd(Condition con) => DB.Conditions.Add(con);
        public bool UserHasCondition(int con_id, int user_id) => DB.Patient_Conditions.FirstOrDefault(u => u.Condition.Id == con_id && u.User.Id == user_id) != null;

        public bool HasConditions(int user_id, string conditions)
        {
            if (!string.IsNullOrEmpty(conditions))
            {
                List<int> cond = conditions.Split(',').Select(n => int.Parse(n.Trim())).ToList();
                return cond.Count > 1 ? cond.All(p => UserHasCondition(p, user_id)) : cond.Any(p => UserHasCondition(p, user_id));
            }
            return true;
        }

        #endregion

        #region Patients

        public List<Patient> PatientsGet(Staff user) => (from s1 in DB.Patients where s1.StaffID == user.Id select s1).ToList();
        public List<Patient> PatientsGetAll() => DB.Patients.ToList();
        public void PatientsAdd(Patient patient) => DB.Patients.Add(patient);
        public Patient PatientGet(int id) => DB.Patients.FirstOrDefault(p => p.UserID == id);

        #endregion

        #region Access Level
        /// <summary>
        /// Add access level to database
        /// </summary>
        /// <param name="a">Access Level to add</param>
        public void AccessLevelAdd(Access_Levels a) => DB.Access_Levels.Add(a);
        public Access_Levels AccessLevelGet(User user) => DB.Access_Levels.FirstOrDefault(u => u.User == user);
        public void AccessLevelClear() => DB.Database.ExecuteSqlCommand("TRUNCATE TABLE [@p0]", "Access_Levels");
        #endregion

        #region Emergency Contact
        /// <summary>
        /// Add emergency contact to database
        /// </summary>
        /// <param name="e">Emergency contact to add</param>
        public void EmergencyContactAdd(Emergency_Contacts e) => DB.Emergency_Contacts.Add(e);
        public void EmergencyContactRemove(Emergency_Contacts e) => DB.Emergency_Contacts.Remove(e);
        #endregion

        #region Appointments


        /// <summary>
        /// Add appointment to database
        /// </summary>
        /// <param name="a">Appointment to add</param>
        public void AppointmentsAdd(Appointment a) => DB.Appointments.Add(a);

        /// <summary>
        /// Get appointment based on reference number
        /// </summary>
        /// <param name="refNum">Associated ref number</param>
        /// <returns></returns>
        public Appointment AppointmentGet(String refNum) => DB.Appointments.FirstOrDefault(a => a.Ref_Number.ToUpper() == refNum.ToUpper());
        public Appointment AppointmentGet(int id) => DB.Appointments.Find(id);
        public void AppointmentRemove(Appointment ap) => Remove(ap.Appointment_Completion, ap);


        public Appointment_Completion AppointmentCompletionGet(int id) => DB.Appointment_Completion.FirstOrDefault(i => i.AppointmentID == id);

        /// <summary>
        /// Get List of appointments for a user
        /// </summary>
        /// <param name="user">Associated user</param>
        /// <returns></returns>
        public List<Appointment> AppointmentsGet(User user) => DB.Users.FirstOrDefault(u => u == user).Patient.Appointments.ToList();

        public List<Appointment> AppointmentsGet() => DB.Appointments.ToList();

        /// <summary>
        /// Get List of appointments from specified staff member
        /// </summary>
        /// <param name="staff">Associated staff user</param>
        /// <returns></returns>
        public List<Appointment> AppointmentsGet(Staff staff) => DB.Appointments.Where(ap => ap.StaffID == staff.Id).ToList();
        /// <summary>
        /// Gets appointments that have appointment date equal to date argument.
        /// </summary>
        /// <param name="staff">Appointments of staff member to retrieve</param>
        /// <param name="day">Day to retrieve appointments for</param>
        /// <returns>Appointments for specified staff member on specified date</returns>
        public List<Appointment> AppointmentsGet(Staff staff, DateTime day) => DB.Appointments.Where(ap => ap.StaffID == staff.Id && ap.Appointment_DateTime.Day == day.Day && ap.Appointment_DateTime.Month == day.Month && ap.Appointment_DateTime.Year == day.Year).ToList();

        public List<Appointment> AppointmentsGet(DateTime day) => DB.Appointments.Where(ap => ap.Appointment_DateTime.Day == day.Day && ap.Appointment_DateTime.Month == day.Month && ap.Appointment_DateTime.Year == day.Year).ToList();

        public List<Appointment> AppointmentsGet(Staff staff, DateTime date1, DateTime date2) => (from ap in DB.Appointments where ap.StaffID == staff.Id where ap.Appointment_DateTime >= date1 where ap.Appointment_DateTime <= date2 select ap).ToList();

        public List<Appointment> AppointmentsGet(DateTime date1, DateTime date2) => (from ap in DB.Appointments where ap.Appointment_DateTime >= date1 where ap.Appointment_DateTime <= date2 select ap).ToList();

        #endregion

        #region Admin

        public bool HasAdmin() => (DB.Access_Levels.OrderBy(a => a.Access_Level >= (int)GlobalVariables.ACCESS_LEVELS.ADMIN).Count() > 0);

        public void AdminAdd(Staff staff)
        {
            Access_Levels access_ = new Access_Levels();
            access_.Access_Level = (int)GlobalVariables.ACCESS_LEVELS.ADMIN;
            access_.Access_Enabled = true;
            access_.User = staff.User;
            DB.Access_Levels.Add(access_);
        }

        #endregion
    }
}
