using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// The class that allows the doctor to add the medication for the patient
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    /// <summary>
    /// PatientMedication is derived from the BasePage
    /// </summary>
    public partial class PatientMedication : BasePage
    {
        public PatientMedication() : base(Permissions.MODIFY_PATIENT_MEDICAL_INFO) { }

        /// <summary>
        /// Loads the page for the medication and runs the methods as soon as the page is loaded
        /// </summary>
        /// <param name="sender">Refers to the object</param>
        /// <param name="e">Refers to the event that occurs </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoggedInUser != null && DB.StaffGet(LoggedInUser) != null) { LoadPatients(); LoadMedications(); }
            DataBindChildren();
        }

        /// <summary>
        /// Method that loads the drop down menu in the format that is specified 
        /// </summary>
        protected void LoadMedications()
        {
            int patient_id = Validator.IsIntegerCorrect(select_medications.SelectedValue.ToString(), -1);
            select_medications.Items.Clear(); /// Removes the list items.
            DB.MedicationsGet().ForEach(i => select_medications.Items.Add(new ListItem() { Text = string.Format("{0} ({1})", i.Name, i.Provider_Info.Name), Value = i.Id.ToString() }));
            CheckButton();
        }

        /// <summary>
        /// Only makes the button availiable if the items are selected
        /// </summary>
        protected void CheckButton() => btnContinue.Enabled = select_patients.Items.Count > 0 && select_medications.Items.Count > 0;

        /// <summary>
        /// This loads the patient for the staff that is logged in.
        /// </summary>
        protected void LoadPatients()
        {
            if (!IsPostBack)
            {
                select_patients.Items.Clear();
                DB.StaffGet(LoggedInUser).Patients.ToList().ForEach(patient => select_patients.Items.Add(new ListItem(string.Format("{0}, {1} ({2})",
                        patient.User.Surname, patient.User.Forename, patient.User.Email),
                        patient.UserID.ToString())));
                CheckButton();
            }
        }

        /// <summary>
        /// When a medication is selected this method returns the medication from the database.
        /// </summary>
        public BusinessObject.Medication GetSelectedMedication
        {
            get
            {
                int val = Validator.IsIntegerCorrect(select_medications.SelectedValue.ToString(), -1);
                return DB.MedicationGet(val);
            }
        }

        /// <summary>
        /// This adds a selected medication to a patient. It ensures a medicition and a patient are selected otherwise it outputs an error message. Once a medication has been selected, all the details
        /// regarding the medicine such as it's name and dosage per day/week is then added to the patients profile.
        /// </summary>
        protected void AddCondition()
        {
            int medication_id = Validator.IsIntegerCorrect(select_medications.SelectedValue.ToString(), -1), patient_id = Validator.IsIntegerCorrect(select_patients.SelectedValue.ToString(), -1);

            if (!(medication_id > -1 || patient_id > -1))
            {
                ShowMessage("Oops!", "an error occured adding this condition to the selected patient, try again...", false, MessageType.ERROR);
                return;
            }

            if (DB.MedicationGet(medication_id) == null || DB.PatientGet(patient_id) == null)
            {
                ShowMessage("Oops!", "an error occured adding this condition to the selected patient, try again...", false, MessageType.ERROR);
                return;
            }


            BusinessObject.Patient_Medications medication = new BusinessObject.Patient_Medications();
            BusinessObject.Medication meds = DB.MedicationGet(medication_id);

            medication.Medication = meds;
            medication.Date_Time = DateTime.Now; /// When the medication was selected, Current time 
            medication.Note = txtAdditionalComments.Text;
            medication.Dosage_Per_Week = Validator.IsIntegerCorrect(txtDosagePerWeek.Text, meds.Max_Dosage_Per_Week);
            medication.Dosage_Per_Day = Validator.IsIntegerCorrect(txtDosagePerDay.Text, meds.Max_Dosage_Per_Day);
            medication.Staff = DB.StaffGet(LoggedInUser);

            /// Add the information to the database related to the database
            DB.PatientGet(patient_id).User.Patient_Medications.Add(medication);
            DB.SaveChanges();

            ShowMessage("Success", "this condition has been added to this patients profile...", true, MessageType.SUCCESS);
            ///Update and load the drop downs again
            LoadMedications();
            LoadPatients();
        }

        /// <summary>
        /// When button is cklicked these methods will run
        /// </summary>
        /// <param name="sender">Refers to the object</param>
        /// <param name="e">Refers to the action</param>
        protected void BtnContinue_Click(object sender, EventArgs e)
        {
            AddCondition();
            HandleReturn();
        }
    }
}