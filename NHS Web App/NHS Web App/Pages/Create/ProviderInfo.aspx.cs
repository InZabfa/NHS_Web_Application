using System;
using System.IO;

/// <summary>
/// Class that adds a provider of medicine
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class ProviderInfo : BasePage
    {
        public ProviderInfo() : base(Permissions.CREATE_PROVIDER_INFO) { }

        public string Return_URL { get; set; }

        /// <summary>
        /// Checks if the information is currently being edited then return the editing id which is -1
        /// </summary>
        public new int EditingID
        {
            get
            {
                if (Request.QueryString["edit"] != null)
                {
                    return DataLayer.Validator.IsIntegerCorrect(Request.QueryString["edit"] ?? "-1".ToString(), -1);
                }
                else
                    return -1;
            }
        }

        /// <summary>
        /// If is editing returns a Boolean true else false 
        /// </summary>
        public new bool IsEditing
        {
            get
            {
                return EditingID > -1 && DB.ProviderGet(EditingID) != null;
            }
        }

        /// <summary>
        /// Runs this when page is loaded
        /// </summary>
        /// <param name="sender">variable that states an object</param>
        /// <param name="e">variable that states an event</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///If return to previous page is requested the id of return is set to 1 and checks that the "return" is not null
            ///if this is correct and then the path is mapped if it is not empty and the return exists then the user is sent back to the page they were before
            if (Request.QueryString["return"] != null)
                if (Request.QueryString["return"].ToString() == "1" && Request.QueryString["return_url"] != null)
                {
                    string return_url = Request.QueryString["return_url"].ToString();
                    if (Server.MapPath(return_url) != string.Empty && File.Exists(Server.MapPath(return_url)))
                    {
                        Return_URL = return_url;
                    }
                }
            /// If user is editing existing practice and it is not a postback then this section is run
            if (IsEditing && !IsPostBack)
            {
                ///button is used that specifies changes
                btnContinue.Text = "Save changes...";
                Title = "Update Provider Info | NHS Management System";
                ///Conects to the database to get all existing elements stored for that provider
                BusinessObject.Provider_Info _Info = DB.ProviderGet(EditingID);
                ///if the information is not null then all of these are inserted into the existing text boxes
                if (_Info != null)
                {
                    txtAddress.Text = _Info.Address;
                    txtEmail.Text = _Info.Email_Address;
                    txtPhoneNumber.Text = _Info.Phone_Number;
                    txtProviderName.Text = _Info.Name;
                }
            }
        }

        /// <summary>
        /// If user wishes to edit the medication then -1 is returned 
        /// </summary>
        public int MedicationID
        {
            get
            {
                return DataLayer.Validator.IsIntegerCorrect(Request.QueryString["medication"] ?? "-1".ToString(), -1);
            }
        }

        /// <summary>
        /// The method that runs the methods after an action has occurred in this case add a provider
        /// </summary>
        /// <param name="sender"> name of the object</param>
        /// <param name="e">Event needs to happen in order for this to run</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            AddProvider();
        }

        /// <summary>
        /// Method that allows the new provider to be added to the database
        /// </summary>
        protected void AddProvider()
        {
            ///Sets local variables for all the inputs
            String name = DataLayer.Validator.FirstLetterToUpper(txtProviderName.Text.Trim());///makes sure the first letter is an upper case
            String address = txtAddress.Text.Trim();
            String email = txtEmail.Text.Trim();
            String phone = txtPhoneNumber.Text.Trim();

            ///Performs all the checks to make sure that the user has added all required information
            if (String.IsNullOrEmpty(name))
            {
                ShowMessage("Oops!", "you must have a name for this provider...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(address))
            {
                ShowMessage("Oops!", "you must have an address for this provider...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(email))
            {
                ShowMessage("Oops!", "you must have an email for this provider...", false, MessageType.ERROR);
                return;
            }

            if (String.IsNullOrEmpty(phone))
            {
                ShowMessage("Oops!", "you must have a phone number for this provider...", false, MessageType.ERROR);
                return;
            }

            /// Sets all the local variables to the elements of the array that they should be stored at.
            BusinessObject.Provider_Info providerInfo = new BusinessObject.Provider_Info()
            {
                Address = address,
                Email_Address = email,
                Name = name,
                Phone_Number = phone
            };

            /// If user is editing the exisitng entry then the methos to update elements is run and then changes are saved to the database
            /// else the new provider is added and all of the entries are filled
            if (IsEditing)
            {
                BusinessObject.Provider_Info pInfo = DB.ProviderGet(EditingID);
                DB.Update(pInfo);
                DB.SaveChanges();
            }
            else
            {
                DB.ProviderInfoAdd(providerInfo);
                DB.SaveChanges();
            }

            CloseMessages();

            /// if the return address is not null then medication can be added to the medicines table
            /// allows to redirect to the new URL decided upon the current page and user 
            if (!string.IsNullOrWhiteSpace(Return_URL))
            {
                BusinessObject.Medication med = new BusinessObject.Medication();
                med = Session["Medicine_Recovery"] as BusinessObject.Medication;
                med.Provider_Info = providerInfo;
                Session["Medicine_Recovery"] = med;
                Response.Redirect(Return_URL + "?refill=1" + (MedicationID > -1 ? "&edit=" + MedicationID : "") + "&providerid=" + DB.ProvidersGet().Find(u => u.Email_Address == email && u.Address == address && u.Name == name && u.Phone_Number == phone).Id);
            }

            ///Prints messages when everything was successfully edited or added
            if (IsEditing)
            {
                ShowMessage("Updated", "successfully updated provider '" + name + "'...", true, MessageType.SUCCESS);
            }
            else
            {
                ShowMessage("Success", "added provider '" + name + "'...", true, MessageType.SUCCESS);
            }
        }
    }
}