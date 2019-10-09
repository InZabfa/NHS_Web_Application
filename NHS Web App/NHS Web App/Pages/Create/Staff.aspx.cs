using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Class that is run to create a staff member
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Staff : BasePage
    {
        public Staff() : base(Permissions.VIEW_STAFF) { }

        /// <summary>
        /// On the page load these methods are run, drop down created and binds data using DataBindChildren().
        /// </summary>
        /// <param name="sender"> name of the object</param>
        /// <param name="e">name of event that occurs</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            dropDownRole();
            DataBindChildren();
        }

        /// <summary>
        /// When the button is pressed these methods will be run
        /// </summary>
        /// <param name="sender">name of object</param>
        /// <param name="e">name of the event that occurred</param>
        protected void saveEntered(object sender, EventArgs e)
        {
            add_Staff();
            dropDownRole();
        }

        /// <summary>
        /// This method creates the new staff member and adds them to the database
        /// </summary>
        protected void add_Staff()
        {
            /// Declares all the local variable names that later will be stored in the database.
            String sName = staffName.Text.Trim();
            String sSurname = staffSurname.Text.Trim();
            /// decides whether the user is male if yes stores as True if not then False
            Boolean sGender = Validator.IsIntegerCorrect(selected_gender.SelectedValue, 1) == 1;
            /// validates the time(date) to make sure that it is in a correct format
            System.DateTime sDOB = Validator.IsDateTimeCorrect(staffDOB.Text);
            String sMail = staffEmail.Text.Trim();
            String sPhNo = staffPhNo.Text.Trim();
            String sAddress = staffAddress.Text.Trim();
            /// Uses the method from the DataLayer to generate a new random password for each new user
            String sPassword = Helper.GenerateRandomPassword(GlobalVariables.PASSWORD_MIN_LENGTH);

            /// Performs checks to make sure all the inputs are there and are correct
            if (String.IsNullOrEmpty(sName))
            {
                ShowMessage("Oops!", "you must provide staff first name...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(sSurname))
            {
                ShowMessage("Oops!", "you must provide staff surname...", false, MessageType.ERROR);
                return;
            }
            //makes sure that the date of birth is entered and also that it is not set to the current time
            if (sDOB == null || sDOB == DateTime.Now)
            {
                ShowMessage("Oops!", "you must provide staff DOB...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(sMail))
            {
                ShowMessage("Oops!", "you must provide staff email...", false, MessageType.ERROR);
                return;
            }
            if (DB.UserGet(sMail) != null)
            {
                ShowMessage("Oops!", "the account with this email already exists...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(sPhNo))
            {
                ShowMessage("Oops!", "you must provide staff phone number...", false, MessageType.ERROR);
                return;
            }
            if (String.IsNullOrEmpty(sAddress))
            {
                ShowMessage("Oops!", "you must provide staff home address...", false, MessageType.ERROR);
                return;
            }


            /// every element name in the database is defined to every element of the local variable
            /// ensures all the elements entered are stored in the correct column of the database
            BusinessObject.User staff = new BusinessObject.User()
            {
                Forename = sName,
                Surname = sSurname,
                Gender = sGender,
                DOB = sDOB,
                Email = sMail,
                Phone_number = sPhNo,
                Address = sAddress,
                /// makes sure that the randomly generated password is saved in the database as an encrypted string
                Password = Encryption.GetSha256(sPassword),
                PracticeID = LoggedInUser.PracticeID
            };

            /// creates a new emergency contact and relates it to the staff member created using their ID
            /// this is done using GetEmergencyContact() method.
            BusinessObject.Emergency_Contacts emergency_contact = GetEmergencyContact(staff);

            /// adds the emergency contact to the database
            staff.Emergency_Contacts.Add(emergency_contact);


            //Add entry into access levels table with selected access number.
            staff.Access_Levels = new BusinessObject.Access_Levels() { Access_Enabled = true, Access_Level = Validator.IsIntegerCorrect(role.SelectedValue.ToString(), 1) };



            /// extra information about the staff member to be added to the database using their created ID
            /// done using the GetStaff() method.
            BusinessObject.Staff more_Info = GetStaff(staff);

            /// adding extra information about the staff to the database
            staff.Staffs.Add(more_Info);

            /// adds the staff member to the user table
            DB.UserAdd(staff);
            /// saves all changes to the database
            DB.SaveChanges();

            /// Emails the new staff a password that was generated
            Helper.Email(LoggedInUser.Practice_Info.Email, sMail, "Your Password", String.Format("<p>Your email is: {0}</p><hr /><br /><p>Your password is: {1}</p>", sMail, sPassword));

            /// When staff member is created the user (Admin) is redirected back to the staff page
            Response.Redirect("~/Pages/Staff.aspx");
        }

        /// <summary>
        /// Method creates, checks the emergency contact information and allows it to be added to the database
        /// </summary>
        /// <param name="user">Uses the ID of original staff member to connect the database keys</param>
        /// <returns>string storing all information about emergency contact</returns>
        // assigns local variables to the IDs of the text boxes that appear on the HTML page
        BusinessObject.Emergency_Contacts GetEmergencyContact(BusinessObject.User user)
        {
            String sEmName = emer_Forname.Text.Trim();
            String sEmSurname = emer_Surname.Text.Trim();
            String sEmRelation = relation.Text.Trim();
            String sEmPhNo = emer_PhNo.Text.Trim();
            String sEmAddress = emer_Address.Text.Trim();

            /// performs all the checks to make sure that all inputs are correct and if they aren't error message is displayed
            if (String.IsNullOrEmpty(sEmName))
            {
                ShowMessage("Oops!", "you must provide staff emergency contact Name...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(sEmSurname))
            {
                ShowMessage("Oops!", "you must provide staff emergency contact Surname...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(sEmRelation))
            {
                ShowMessage("Oops!", "you must provide staff relation to the Emergency contact...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(sEmPhNo))
            {
                ShowMessage("Oops!", "you must provide staff emergency contact phone number...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(sEmAddress))
            {
                ShowMessage("Oops!", "you must provide staff emergency contact Address...", false, MessageType.ERROR);
                return null;
            }

            /// all the local variables are assigned to the columns in the database, so that they could be added to the database
            BusinessObject.Emergency_Contacts staffEmContact = new BusinessObject.Emergency_Contacts()
            {
                Forename = sEmName,
                Surname = sEmSurname,
                Relation = sEmRelation,
                Phone_Number = sEmPhNo,
                Address = sEmAddress,
            };
            /// returns the elements that will be stores in the emergency contacts table as a string
            return staffEmContact;
        }

        /// <summary>
        /// Performs a calculation to find the long that will represent the working days of the staff
        /// </summary>
        /// <returns>Long of 1 and 0, representing working days</returns>
        // method that stores the working days of the staff member as a long of 0 and 1.
        public string GetDaysValues()
        {
            ///<summary>
            /// creates a list of items that will store the days staff are working
            /// each day has unique value but is stored as a long so validator changes it to an integer where it goes through the array of days and lists all of them
            /// it takes the id of the selected day
            /// </summary>
            List<int> items = working_days.GetSelectedIndices().ToList().Select(i => Validator.IsIntegerCorrect(working_days.Items[i].Value, 0)).ToList();

            /// default long of days that is edited depending on the days chosen
            long DaysLong = 0000000;

            /// for each day in the list of items if it has the item with a number in the if statement means that day was selected
            /// this will change that day to a 1 instead of 0, for it to be stored as on long 
            foreach (var item in items)
            {
                if (item == 0) DaysLong += 1000000; //Monday
                if (item == 1) DaysLong += 0100000; //Tuesday
                if (item == 2) DaysLong += 0010000; //Wednesday
                if (item == 3) DaysLong += 0001000; //Thursday
                if (item == 4) DaysLong += 0000100; //Friday
                if (item == 5) DaysLong += 0000010; //Saturday
                if (item == 6) DaysLong += 0000001; //Sunday
            }


            /// Does the check to make sure that the items selected either at least has a one day selected or no more than all days selected
            if (!(DaysLong > 1111111 && DaysLong < 0000000)) return DaysLong.ToString("0000000");
            else return "0000000";
        }

        /// declaring local names that will store all the inputs for the extra information about staff members
        BusinessObject.Staff GetStaff(BusinessObject.User user)
        {
            string contract = contract_select.SelectedValue.ToString();/// takes the drop down selected item and stores it as a string
            string working_days = GetDaysValues(); /// takes the values for the days using the method GetDaysValues()
            int working_hours = Validator.IsIntegerCorrect(wrk_hours.Text, 40); /// makes sure that the value is an integer if not default is set
                                                                                /// validates if the selected is full time if it is returns true else returns false meaning part time
            Boolean ftpt = Validator.IsIntegerCorrect(ft_pt.SelectedValue, 1) == 1;
            /// uses the database table that stores roles and allows to pick the role and setting their access level
            String role_Access = DB.AccessTypesGet(Validator.IsIntegerCorrect(role.Text, 1)).Name;

            /// Checks that all of the inputs are not null or empty
            if (byte.Equals(working_days, null))
            {
                ShowMessage("Oops!", "you must provide staff working days...", false, MessageType.ERROR);
                return null;
            }
            if (int.Equals(working_hours, null))
            {
                ShowMessage("Oops!", "you must provide staff working hours...", false, MessageType.ERROR);
                return null;
            }
            if (String.IsNullOrEmpty(role_Access))
            {
                ShowMessage("Oops!", "you must provide staff role...", false, MessageType.ERROR);
                return null;
            }

            /// sets all the columns of the database to the local elements to make sure they are stores correctly
            BusinessObject.Staff staff_More = new BusinessObject.Staff()
            {
                Contract_type = contract,
                Working_days = working_days,
                Working_hours = working_hours,
                Staff_Role = role_Access
            };

            /// returns the extra information about the staff
            return staff_More;
        }

        /// <summary>
        /// updates the dropdown list of the roles that are populated as soon as the page loads
        /// </summary>
        protected void dropDownRole()
        {
            /// if the page is loaded for the first time the dropdown should be populated
            if (!IsPostBack)
            {
                /// for each row in the roles table gets all of the roles
                foreach (var item in DB.AccessTypesGetAll())
                {
                    /// if access level is admin or above
                    if (item.AccessLevel >= 6)
                    {
                        /// the role is an admin
                        if (GetRole() == ROLES.ADMIN)
                        {
                            /// the name of the role and their access level is presented
                            role.Items.Add(new ListItem(item.Name, item.AccessLevel.ToString()));
                        }
                    }
                    else
                        /// Add role items into the list by their name and include the access integer as a string
                        role.Items.Add(new ListItem(item.Name, item.AccessLevel.ToString()));
                }
            }
        }
    }
}