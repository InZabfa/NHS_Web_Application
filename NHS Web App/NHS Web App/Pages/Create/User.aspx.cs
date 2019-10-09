using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Class that allows you to add a user that doesn't have to be a specific staff member.
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class User : BasePage
    {
        public User() : base(Permissions.CREATE_PATIENT) { }
        protected void Page_Load(object sender, EventArgs e) { }

        /// <summary>
        /// When continue is pressed the add_User method will run
        /// </summary>
        /// <param name="sender">name of the object</param>
        /// <param name="e">makes sure that the event occurs </param>
        protected void saveEntered(Object sender, EventArgs e)
        {
            add_User();
        }

        /// <summary>
        /// Methods that adds a new user to the database
        /// </summary>
        protected void add_User()
        {
            ///Declares all the local variables and performs required checks where necessary
            String uName = userName.Text.Trim();
            String uSurname = userSurname.Text.Trim();
            Boolean uGender = Validator.IsIntegerCorrect(userGender.SelectedValue, 1) == 1;
            DateTime uDOB = Validator.IsDateTimeCorrect(userDOB.Text);
            String uMail = userEmail.Text.Trim();
            String uPhNo = userPhNo.Text.Trim();
            String uAddress = userAddress.Text.Trim();
            String uPassword = Helper.GenerateRandomPassword(GlobalVariables.PASSWORD_MIN_LENGTH);

            ///performs all the checks to make sure the input is there and is correct
            if (String.IsNullOrEmpty(uName))
            {
                ShowMessage("Oops!", "you must provide user first name...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(uSurname))
            {
                ShowMessage("Oops!", "you must provide user surname name...", false, MessageType.ERROR);
                return;
            }
            if (uDOB == null || uDOB == DateTime.Now)
            {
                ShowMessage("Oops!", "you must provide user DOB...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(uMail))
            {
                ShowMessage("Oops!", "you must provide user Email...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(uPhNo))
            {
                ShowMessage("Oops!", "you must provide user Phone number...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(uAddress))
            {
                ShowMessage("Oops!", "you must provide user Address...", false, MessageType.ERROR);
                return;
            }

            ///Declares that local variables are the specific columns in the database
            ///ensures that all the inputs are saved in the right place
            BusinessObject.User user = new BusinessObject.User()
            {
                Forename = uName,
                Surname = uSurname,
                Gender = uGender,
                DOB = uDOB,
                Email = uMail,
                Phone_number = uPhNo,
                Address = uAddress,
                Password = Encryption.GetSha256(uPassword),
                PracticeID = LoggedInUser.PracticeID
            };

            ///Create access level for patient
            user.Access_Levels = new BusinessObject.Access_Levels() { Access_Enabled = true, Access_Level = 1 };

            ///Declares and runs the method to create emergency contact that is connected to the main user
            BusinessObject.Emergency_Contacts user_Emergency = EmergencyContact(user);
            ///Adds emergency contact to the database
            user.Emergency_Contacts.Add(user_Emergency);
            ///Adds everything to the database and saves all the changes
            DB.UserAdd(user);
            DB.SaveChanges();

            /// Sends the user a password that was generated
            Helper.Email(LoggedInUser.Practice_Info.Email, uMail, "Your Password", String.Format("<p>Your email is: {0}</p><hr /><br /><p>Your password is: {1}</p>", uMail, uPassword));
            ///Redirect the current user back to the main page
            Response.Redirect("~/Pages/Patients.aspx");

        }

        /// <summary>
        /// Method that declares information on emergency contact, performs checks, and
        /// returns the string that will be added to the database
        /// </summary>
        /// <param name="user">runs the user ID to connect the foreign and primary keys</param>
        /// <returns>User Emergency as a string</returns>
        BusinessObject.Emergency_Contacts EmergencyContact(BusinessObject.User user)
        {
            ///Declaring local variables for all of the information entered about the emergency contact
            String uEmerName = userEmer_Forname.Text.Trim();
            String uEmerSurname = userEmer_Surname.Text.Trim();
            String uEmerRelation = relationToUser.Text.Trim();
            String uEmerNumber = userEmer_PhNo.Text.Trim();
            String uEmerAddress = userEmer_Address.Text.Trim();

            ///Performs all the checks making sure there is some input and few other extra checks
            if (String.IsNullOrEmpty(uEmerName))
            {
                ShowMessage("Oops!", "you must provide user Emergency contact Forename...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(uEmerSurname))
            {
                ShowMessage("Oops!", "you must provide user Emergency contact Surname...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(uEmerRelation))
            {
                ShowMessage("Oops!", "you must provide user Emergency contact Relation to user...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(uEmerNumber))
            {
                ShowMessage("Oops!", "you must provide user Emergency contact Phone number...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(uEmerAddress))
            {
                ShowMessage("Oops!", "you must provide user Emergency contact Address...", false, MessageType.ERROR);
                return null;
            }

            ///Declaring that all the local variables are equal to the ones in the database making sure that all are added correctly
            BusinessObject.Emergency_Contacts userEmContact = new BusinessObject.Emergency_Contacts()
            {
                Forename = uEmerName,
                Surname = uEmerSurname,
                Relation = uEmerRelation,
                Phone_Number = uEmerNumber,
                Address = uEmerAddress,
            };

            ///returns the string that has all the required information that needs to be stored in the database
            return userEmContact;
        }
    }

}
