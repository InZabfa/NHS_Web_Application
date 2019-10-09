using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// This class is responsible for adding conditions to the existing patients.
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Condition : BasePage /// Condition derives from BasePage.
    {
        public Condition() : base(Permissions.MODIFY_CONDITION) { }

        /// <summary>
        /// When the page is loaded these methods will run
        /// </summary>
        /// <param name="sender">Creates an object</param>
        /// <param name="e">Waits for the even to occurred</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Performs checks on the logged in user if it is an appropriate staff member then condition can be added otherwise it is disabled
            if (!(LoggedInUser != null))
                if (!(DB.StaffGet(LoggedInUser) != null))
                    disableControls();
        }

        /// <summary>
        /// This method is used to prevent users from accessing features that they don't have the required access level for. 
        /// It prints an error message to let them know.
        /// </summary>
        protected void disableControls()
        {
            ShowMessage("Oops!", "you don't have the correct access level to add conditions...", false, MessageType.ERROR);
            btnContinue.Enabled = false;
            txtAdditionalInfo.Enabled = false;
            txtMedicalCondition.Enabled = false;
        }

        /// <summary>
        /// Method runs when a button is clicked. It is responsible for allowing staff to add a condition.
        /// </summary>
        /// <param name="sender">Creates an object</param>
        /// <param name="e">Event occurrence</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            /// Makes sure a user is logged in (with the correct access level).
            if (DB.StaffGet(LoggedInUser) != null)
                ///Checks to make sure that the condition is not null
                if (!String.IsNullOrEmpty(txtMedicalCondition.Text))
                {
                    string med_name = txtMedicalCondition.Text; /// A string variable for medical condition. 
                    string additional_det = txtAdditionalInfo.Text; /// A string variable for any additional information that may be added.

                    BusinessObject.Staff staff = DB.StaffGet(LoggedInUser); ///Get the logged in user information

                    ///Sets the local input variables to the database variables so that they can be added to the table.
                    BusinessObject.Condition condition = new BusinessObject.Condition()
                    {
                        Date_Added = DateTime.Now, /// time of when the condition was added.
                        Staff = staff, /// Staff member who added it.
                        Additional_Info = additional_det, /// Any additional information that was added.
                        Name = med_name /// the name of the medical condition.
                    };

                    DB.ConditionAdd(condition); /// Adds the condition to the database.
                    DB.SaveChanges(); /// Saves the changes made to the table.

                    ///Shows the message that it has been successfully added
                    ShowMessage("Success", "added the condition...", true, MessageType.SUCCESS);
                }
                else
                    ShowMessage("Oops!", "you need to type a name for this medical condition...", false, MessageType.ERROR);
            else
                disableControls();///if they don't have appropriate access then the controls are disabled
        }
    }
}