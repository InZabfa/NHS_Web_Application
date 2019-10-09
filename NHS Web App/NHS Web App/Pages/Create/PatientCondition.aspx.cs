using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// This class is responsible for adding conditions to the patients profile
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class PatientCondition : BasePage
    {
        public PatientCondition() : base(Permissions.MODIFY_PATIENT_MEDICAL_INFO) { }

        /// <summary>
        /// Handles and runs the methods when the page is loaded for the first time.
        /// </summary>
        /// <param name="sender">Refers to an object</param>
        /// <param name="e">Refers to an action</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///If the user is logged in and is a staff member the methods can run
            if (LoggedInUser != null && DB.StaffGet(LoggedInUser) != null) { LoadPatients(); LoadConditions(); }
        }

        /// <summary>
        /// Method loads all the conditions that are on the database are presented 
        /// </summary>
        protected void LoadConditions()
        {
            int patient_id = Validator.IsIntegerCorrect(select_conditions.SelectedValue.ToString(), -1);
            select_conditions.Items.Clear();
            DB.ConditionsGet().Where(u => !DB.PatientConditionAlreadyHas(u, patient_id)).ToList().ForEach(u => select_conditions.Items.Add(new ListItem(u.Name, u.Id.ToString())));
            CheckButton();
        }

        /// <summary>
        /// This method checks if the condition was selected if it has then the button will be enabled
        /// </summary>
        protected void CheckButton() => btnContinue.Enabled = select_patients.Items.Count > 0 && select_conditions.Items.Count > 0;

        /// <summary>
        /// Method loads all the patients to the dropdown in the format specified 
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
        /// Method that checks and adds the condition to the patient and saves the information to the database
        /// </summary>
        protected void AddCondition()
        {
            int condition_id = Validator.IsIntegerCorrect(select_conditions.SelectedValue.ToString(), -1), patient_id = Validator.IsIntegerCorrect(select_patients.SelectedValue.ToString(), -1);

            if (!(condition_id > -1 || patient_id > -1))
            {
                ShowMessage("Oops!", "an error occured adding this condition to the selected patient, try again...", false, MessageType.ERROR);
                return;
            }

            if (DB.ConditionGet(condition_id) == null || DB.PatientGet(patient_id) == null)
            {
                ShowMessage("Oops!", "an error occured adding this condition to the selected patient, try again...", false, MessageType.ERROR);
                return;
            }


            BusinessObject.Patient_Conditions condition = new BusinessObject.Patient_Conditions();

            condition.Condition = DB.ConditionGet(condition_id);
            condition.Date_Time = DateTime.Now;
            condition.Note = txtAdditionalComments.Text;
            condition.Staff = DB.StaffGet(LoggedInUser);

            DB.PatientGet(patient_id).User.Patient_Conditions.Add(condition);
            DB.SaveChanges();

            ShowMessage("Success", "this condition has been added to this patients profile...", true, MessageType.SUCCESS);
            LoadConditions();
            LoadPatients();
        }

        /// <summary>
        /// When buttons clicked the methods are run to add the condition entered and then send the user back to the previous page
        /// </summary>
        /// <param name="sender">Refers to an object</param>
        /// <param name="e">Refers to an action</param>
        protected void BtnContinue_Click(object sender, EventArgs e)
        {
            AddCondition();
            HandleReturn();
        }
    }
}