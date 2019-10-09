using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Class that is responsible for adding a new practice to the database that would allow to transfer patients to new practices
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Practice : BasePage
    {
        protected Practice() : base(MedicalPermissions()) { }

        /// <summary>
        /// This method runs when the button on the page is clicked
        /// </summary>
        /// <param name="sender">takes in an object</param>
        /// <param name="e">Needs an action to occur</param>
        protected void btn_Continue_Clicked(object sender, EventArgs e)
        {
            addPractice();
        }

        /// <summary>
        /// This method is responsible for adding the information abut the practice to the database
        /// </summary>
        protected void addPractice()
        {
            /// Declaring local variables that will store the user input
            String pName = DataLayer.Validator.FirstLetterToUpper(practiceName.Text.Trim());
            String pNumber = practicePhNo.Text.Trim();
            String pEmail = practiceEmail.Text.Trim();
            String pAddress = practiceAddress.Text.Trim();

            /// Performs a check to make sure that there is input in the system
            if (String.IsNullOrEmpty(pName))
            {
                ShowMessage("Oops!", "you must provide the name of the practice...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(pNumber))
            {
                ShowMessage("Oops!", "you must provide the practice phone number...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(pEmail))
            {
                ShowMessage("Oops!", "you must provide an email of the practice...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(pAddress))
            {
                ShowMessage("Oops!", "you must provide the address of the practice...", false, MessageType.ERROR);
                return;
            }

            /// Specifies which local variables correspond to which columns in the database
            BusinessObject.Practice_Info practiceInfo = new BusinessObject.Practice_Info()
            {
                Name = pName,
                Phone_Number = pNumber,
                Email = pEmail,
                Address = pAddress
            };

            ///Adds the information to the database
            DB.PracticeAdd(practiceInfo);
            /// Saves changes to the database
            DB.SaveChanges();
            ///Clears all the text boxes filled in by the user
            ClearControls(practiceName, practiceEmail, practicePhNo, practiceAddress);
            CloseMessages();
            ///Sends a message that the practice was added to the database
            ShowMessage("Success", "added a Practice '" + pName + "'...", true, MessageType.SUCCESS);
        }
    }
}