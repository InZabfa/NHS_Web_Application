using DataLayer;
using NHS_Web_App.Controls;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Pages.View
{
    /// <summary>
    /// Profile is derived from BasePage
    /// </summary>
    public partial class Profile : BasePage
    {

        public Profile() : base(MedicalPermissions()) { }

        /// <summary>
        /// This method loads the profile of the user when a user is logged in.
        /// </summary>
        /// <param name="sender">Refers to an object</param>
        /// <param name="e">Refers to an action</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HasAccount())
                Response.Redirect(Helper.PageAddress(Helper.Pages.OVERVIEW));
            else
            {
                LoadPatientNotes();
                LoadPatientConditions();
                LoadPatientAppointments();
                LoadPatientMedications();
                LoadEmergencyContacts();

                if (!HasEmergencyContacts())
                {
                    ShowMessage("Problem", "this patient doesn't have any emergency contacts...", false, MessageType.ERROR); /// Message is printed if a patient doesnt have an emergency contanct.
                }
            }
        }

        /// <summary>
        /// This method loads the notes that a member of staff inputs for a patient. The data and time of when the note is added is also provided.
        /// </summary>
        protected void LoadPatientNotes()
        {
            patient_notes.Controls.Clear();
            foreach (var item in GetUser.Patient_Notes)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("Note added {0} at {1}", item.Added_DateTime.ToShortDateString(), item.Added_DateTime.ToShortTimeString()), this);
                builder.AddHtml("<p>{0}</p>", item.Note);
                builder.AddHtml("<hr />");
                builder.AddProperty("Added", "{0} at {1}", item.Added_DateTime.ToShortDateString(), item.Added_DateTime.ToShortTimeString());
                builder.AddProperty("Doctor", "{0}, {1}", item.Staff.User.Surname, item.Staff.User.Forename);
                Button btnRemove = new Button() { Text = "Remove note...", CssClass = "button" };
                btnRemove.Click += (s, e) => RemoveNote(item);
                HyperLink btnEdit = new HyperLink() { Text = "Edit...", CssClass = "button", NavigateUrl = string.Format("{0}?edit={1}", Helper.PageAddress(Helper.Pages.CREATE_NOTE), item.Id) };
                builder.AddControl(btnEdit, btnRemove);
                patient_notes.Controls.Add(builder.ToObject());
            }
        }

        /// <summary>
        /// This method loads the patients medication. It provides information about the provider, dosage and 
        /// </summary>
        protected void LoadPatientMedications()
        {
            medications_list.Controls.Clear();
            foreach (var item in GetUser.Patient_Medications)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(item.Medication.Name, this);

                builder.AddProperty("Provider", item.Medication.Provider_Info.Name);
                builder.AddProperty("Max dosage per week", item.Medication.Max_Dosage_Per_Week.ToString());
                builder.AddProperty("Max dosage per day", item.Medication.Max_Dosage_Per_Day.ToString());
                builder.AddProperty("Quantity", item.Medication.Stock.Quantity.ToString());
                builder.AddHtml("<hr />");
                builder.AddProperty("Note:", item.Note);

                Button btnRemove = new Button() { Text = "Remove...", CssClass = "button" };
                btnRemove.Click += (s, e) => RemoveMedication(item);
                builder.AddControl(btnRemove);
                medications_list.Controls.Add(builder.ToObject());
            }
        }

        protected void LoadPatientAppointments()
        {
            appointments_list.Controls.Clear();
            var query = (from i in GetUser.Patient.Appointments where i.Appointment_Completion.Status < 2 select i);
            foreach (var item in query)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("<strong>{0}, {1}</strong> - {2} - {3}", item.Patient.User.Forename, item.Patient.User.Surname, item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString()), this);
                builder.AddProperty("Date", item.Appointment_DateTime.ToString("dd/mm/yyyy"));
                builder.AddProperty("Time", "{0} - {1}", item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString());
                builder.AddProperty("Room", item.Room);
                builder.AddHtml("<hr />");
                builder.AddProperty("Reference", item.Ref_Number);
                Button btnRemove = new Button() { Text = "Remove appointment...", CssClass = "button" };
                btnRemove.Click += (s, e) => RemoveAppointment(item);

                builder.AddControl(btnRemove);
                appointments_list.Controls.Add(builder.ToObject());
            }
        }

        protected void LoadEmergencyContacts()
        {
            emergency_contacts.Controls.Clear();
            foreach (var item in GetUser.Emergency_Contacts)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("{0}, {1}", item.Surname, item.Forename), this);
                builder.AddProperty("Phone", @"<a href=""tel:{0}"">{0}</a>", item.Phone_Number);
                builder.AddProperty("Relation", item.Relation);
                builder.AddProperty("Address", item.Address);

                Button btnRemove = new Button() { Text = "Remove...", CssClass = "button" };
                btnRemove.Click += (s, e) => RemoveEmergencyContact(item);

                emergency_contacts.Controls.Add(builder.ToObject());
            }
        }

        protected void LoadPatientConditions()
        {
            conditions_list.Controls.Clear();
            foreach (var item in GetUser.Patient_Conditions)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(item.Condition.Name, this);
                builder.AddProperty("Doctor", "{0}, {1}", item.Staff.User.Surname, item.Staff.User.Forename);
                builder.AddProperty("Added", item.Date_Time.ToShortDateString());
                builder.AddHtml("<hr />");

                builder.AddProperty("Note", item.Note);
                Button btnRemove = new Button() { Text = "Remove condition...", CssClass = "button" };
                btnRemove.Click += (s, e) => RemoveCondition(item);
                builder.AddControl(btnRemove);
                conditions_list.Controls.Add(builder.ToObject());
            }
        }

        protected bool HasAccount() => GetUser != null;
        protected bool HasConditions() => GetUser.Patient_Conditions.Count > 0;
        protected bool HasNotes() => GetUser.Patient_Notes.Count > 0;
        protected bool HasMedication() => GetUser.Patient_Medications.Count > 0;
        protected bool HasAppointments() => GetUser.Patient.Appointments.Count > 0;
        protected bool HasEmergencyContacts() => GetUser.Emergency_Contacts.Count > 0;

        protected void RemoveEmergencyContact(BusinessObject.Emergency_Contacts c)
        {
            DB.EmergencyContactRemove(c);
            DB.SaveChanges();
            CloseMessages();
            LoadEmergencyContacts();
            ShowMessage("Removed", "successfully removed the contact from this profile..,", true, MessageType.SUCCESS);
        }

        protected void RemoveMedication(BusinessObject.Patient_Medications med)
        {
            DB.PatientMedicationRemove(med);
            DB.SaveChanges();
            CloseMessages();
            LoadPatientMedications();
            ShowMessage("Removed", "successfully removed the medicine from this profile..,", true, MessageType.SUCCESS);
        }

        protected void RemoveAppointment(BusinessObject.Appointment app)
        {
            DB.AppointmentRemove(app);
            DB.SaveChanges();
            CloseMessages();
            LoadPatientAppointments();
            ShowMessage("Removed", "successfully removed the appointment from this profile..,", true, MessageType.SUCCESS);
        }

        protected void RemoveCondition(BusinessObject.Patient_Conditions condition)
        {
            DB.PatientConditionRemove(condition);
            DB.SaveChanges();
            CloseMessages();
            LoadPatientConditions();
            ShowMessage("Removed", "successfully removed the condition from this profile..,", true, MessageType.SUCCESS);
        }

        protected void RemoveNote(BusinessObject.Patient_Notes note)
        {
            DB.PatientNoteRemove(note);
            DB.SaveChanges();
            LoadPatientNotes();
            CloseMessages();
            LoadPatientNotes();
            ShowMessage("Removed", "successfully removed the note from this profile..,", true, MessageType.SUCCESS);
        }

        protected BusinessObject.User GetUser
        {
            get
            {
                if (Request.QueryString["id"] != null)
                {
                    int val = Validator.IsIntegerCorrect(Request.QueryString["id"], -1);
                    if (val > -1)
                        return DB.UserGet(val);
                    else
                        return null;
                }
                return null;
            }
        }

        protected void btnArrived_Click(object sender, EventArgs e)
        {
            int id = QueueHandler.GetCurrentAppointment(GetUser.Patient).AppointmentID;
            DB.AppointmentCompletionGet(id).Status = 2;
            DB.SaveChanges();
        }

        protected void btnAppointmentComplete_Click(object sender, EventArgs e)
        {
            int id = QueueHandler.GetCurrentAppointment(GetUser.Patient).AppointmentID;
            DB.AppointmentCompletionGet(id).Status = 3;
            DB.SaveChanges();
        }
    }
}