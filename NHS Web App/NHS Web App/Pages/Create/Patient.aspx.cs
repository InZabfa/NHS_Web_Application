using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// This class is responsible for allocating each new user to the doctor
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Patient : BasePage
    {
        public Patient() : base(Permissions.VIEW_PATIENTS) { }

        /// <summary>
        /// Runs all the methods when the page is first loaded
        /// </summary>
        /// <param name="sender">References an object</param>
        /// <param name="e">References the action EventArgs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            updateDropdowns();
            updateControls();
        }

        /// <summary>
        /// Updates the button in case there are no elements selected from the dropdown the button will be disabled
        /// </summary>
        protected void updateControls()
        {
            btnContinue.Enabled = select_patients.Items.Count > 0 && select_doctors.Items.Count > 0;
        }

        /// <summary>
        /// This method populates and updates both drop downs on the page
        /// </summary>
        protected void updateDropdowns()
        {
            if (!IsPostBack)
            {
                /// Clears all the elements from the drop down
                select_patients.Items.Clear();
                select_doctors.Items.Clear();

                /// Goes through every user in the table and checked if they are not been appointed a doctor they are inserted into the drop down in the format specified
                foreach (BusinessObject.User us in (from u1 in DB.UserGetAllNonStaff() where u1 != LoggedInUser && u1.Patient == null && !(u1.Staffs.Count > 0) select u1))
                {
                    select_patients.Items.Add(new ListItem(string.Format("{0}, {1} ({2})", us.Forename, us.Surname, us.Email), us.Id.ToString(), true));
                }
                /// Goes through every staff member in the practice and displays them in the drop down
                foreach (BusinessObject.Staff staff in DB.StaffGetAll())
                {
                    select_doctors.Items.Add(new ListItem(string.Format("{0}, {1} ({2})", staff.User.Surname, staff.User.Forename, staff.User.Email), staff.Id.ToString(), true));
                }

                /// Performs a check if there is patient selected then sets that index to the 0
                if (select_patients.Items.Count > 0)
                    select_patients.SelectedIndex = 0;
                /// Performs a check if there is a doctor selected then sets that index to the 0
                if (select_doctors.Items.Count > 0)
                    select_doctors.SelectedIndex = 0;

                /// Allows selection if there are items in the list
                select_doctors.Enabled = select_doctors.Items.Count > 0;
                select_patients.Enabled = select_patients.Items.Count > 0;
            }
        }

        /// <summary>
        /// When the button is clicked the information is saved and added to the database
        /// </summary>
        /// <param name="sender">References an object sender</param>
        /// <param name="e">references an event e</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                /// Set the selection to null.
                BusinessObject.User patient = null;
                BusinessObject.Staff staff = null;
                /// gets users and staff members 
                patient = DB.UserGet(int.Parse(select_patients.SelectedValue));
                staff = DB.StaffGet(int.Parse(select_doctors.SelectedValue));

                /// If the patient and staff member is selected then this if statement is running
                if (patient != null && staff != null)
                {
                    /// Adds the patient to the patient table using all the required information 
                    DB.PatientsAdd(new BusinessObject.Patient() { UserID = patient.Id, StaffID = staff.Id, Email_Notifications = chckEmailNotifications.Checked });
                    /// Saves all the changes to the database
                    DB.SaveChanges();
                    ///Remove an item from the list that was selected
                    select_patients.Items.RemoveAt(select_patients.SelectedIndex);
                    /// Show the message that the action has been successful
                    ShowMessage("Success", String.Format("successfully appointed {0} to {1}...", patient.Forename, staff.User.Forename), true, MessageType.SUCCESS);
                }
                else /// Else show a message that something went wrong, therefore no changes were made
                {
                    ShowMessage("Oops!", "an error occured. Please try again...", true, MessageType.ERROR);
                }
            }
            catch (Exception ex) /// if anything goes wrong then the message about the error is shown
            {
                ShowMessage("Oops!", ex.Message, true, MessageType.ERROR);
                throw ex;
            }
            /// Updates all the controls after the changes were made
            updateDropdowns();
            updateControls();
        }
        /// <summary>
        /// Declares the drop-down methods but doesn't perform any actions
        /// </summary>
        /// <param name="sender">Refers to an object</param>
        /// <param name="e">Refers to an event</param>
        protected void select_doctors_SelectedIndexChanged(object sender, EventArgs e) { }
        /// <summary>
        /// Declares the drop-down method but doesn't perform any actions
        /// </summary>
        /// <param name="sender">Refers to an object</param>
        /// <param name="e"> Refers to an event</param>
        protected void select_patients_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}