using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Class that is responsible for adding medication to the database
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Medicine : BasePage
    {

        public Medicine() : base(Permissions.VIEW_MEDICINES) { }

        public BusinessObject.Medication Modify_Medication;

        /// <summary>
        /// This method runs when the page is loaded
        /// </summary>
        /// <param name="sender">States the object</param>
        /// <param name="e">States the event that occurred</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///Shows a message to make sure that the provider exists before the medication is added
            CloseMessages();
            ShowMessage("Info", "before adding a medication, ensure you've added the provider information...", true, MessageType.NONE);

            ///loads all of the medication providers that are in the database
            LoadProviders();

            if (!IsPostBack)
            {
                DataBindChildren();///binds all the data together

                if (IsEditing)
                    btnContinue.Text = "Save Changes..."; ///Show button if editing the medication

                ///Change the URL to the editing mode or to replenish the medication that is running low
                Modify_Medication = IsEditing && Request.QueryString["refill"] == null ? DB.MedicationGet(DataLayer.Validator.IsIntegerCorrect(Request.QueryString["edit"].ToString(), -1)) : Session["Medicine_Recovery"] as BusinessObject.Medication;

                ///Performs multiple checks to make sure that Editing/Replenishing URL is not null, to set the new input to the current one
                ///to make sure that the changes could saved to the database
                int val = -1;
                if (Request.QueryString["refill"] != null || Request.QueryString["edit"] != null)
                    val = Request.QueryString["refill"] == null ? DataLayer.Validator.IsIntegerCorrect(Request.QueryString["edit"].ToString(), -1) : DataLayer.Validator.IsIntegerCorrect(Request.QueryString["refill"].ToString(), -1);
                if (Modify_Medication != null && val > -1)
                {
                    txtMedicineName.Text = Modify_Medication.Name;
                    txtMaxDosagePerDay.Text = Modify_Medication.Max_Dosage_Per_Day.ToString();
                    txtMaxDosagePerWeek.Text = Modify_Medication.Max_Dosage_Per_Week.ToString();
                    txtQuantity.Text = Modify_Medication.Stock.Quantity.ToString();
                }
            }
        }

        /// <summary>
        /// Get the medication that is being edited using their id
        /// </summary>
        public BusinessObject.Medication EditingObject
        {
            get
            {
                return DB.MedicationGet(EditingID);
            }
        }

        /// <summary>
        /// Determines whether select_provider has a selected item.
        /// </summary>
        /// <returns>True or false depending if there are providers</returns>
        protected bool HasProviders()
        {
            return (select_provider.SelectedItem != null);
        }

        /// <summary>
        /// Adds the medicine with entered details to the Database.
        /// Performs multiple checks to make sure that the input information is correct.
        /// </summary>
        public void AddMedicine()
        {
            try
            {
                CloseMessages();
                String name = txtMedicineName.Text;
                int max_dosage_per_week = DataLayer.Validator.IsIntegerCorrect(txtMaxDosagePerWeek.Text, 2), max_dosage_per_day = DataLayer.Validator.IsIntegerCorrect(txtMaxDosagePerDay.Text, 3);
                int provider_id = DataLayer.Validator.IsIntegerCorrect(select_provider.SelectedValue, -1);

                ///Performs all the checks that the input is correct
                if (String.IsNullOrWhiteSpace(name))
                    ShowMessage("Oops!", "you must enter a name for this medication...", false, MessageType.WARNING);

                if (!(provider_id > -1))
                    ShowMessage("Oops!", "there was an error with the selected provider information...", false, MessageType.ERROR);

                if (max_dosage_per_day < 0)
                    ShowMessage("Oops!", "maximum dosage per day cannot be less than 0...", true, MessageType.ERROR);

                if (max_dosage_per_week < 0)
                    ShowMessage("Oops!", "maximum dosage per week cannot be less than 0...", true, MessageType.ERROR);

                ///If currently in not editing mode then all the information of the current local variables are added to the database
                if (!IsEditing)
                {
                    BusinessObject.Medication medication = new BusinessObject.Medication()
                    {
                        Date_Added = DateTime.Now,
                        Staff = DB.StaffGet(LoggedInUser),
                        Max_Dosage_Per_Day = max_dosage_per_day,
                        Max_Dosage_Per_Week = max_dosage_per_week,
                        Name = name,
                        Provider_Info = DB.ProviderGet(provider_id),
                        Stock = new BusinessObject.Stock() { Quantity = DataLayer.Validator.IsIntegerCorrect(txtQuantity.Text, 100) }
                    };
                    DB.MedicationAdd(medication);
                }
                else ///if is editing then get all the existing items and make them equal to the new ones and save all the changes to the database
                {
                    BusinessObject.Medication old_med = EditingObject;
                    BusinessObject.Medication get_med = old_med;
                    get_med.Date_Added = DateTime.Now;
                    get_med.Staff = DB.StaffGet(LoggedInUser);
                    get_med.Max_Dosage_Per_Day = max_dosage_per_day;
                    get_med.Max_Dosage_Per_Week = max_dosage_per_week;
                    get_med.Name = name;
                    get_med.Stock.Quantity = DataLayer.Validator.IsIntegerCorrect(txtQuantity.Text, 100);
                    get_med.ProviderID = provider_id;
                    DB.Update(get_med);
                }
                DB.SaveChanges();

                ///Show message depending on if it was an update to the existing item or creating a new medication
                if (IsEditing)
                    ShowMessage("Updated", "successfully updated '" + name + "'...", true, MessageType.SUCCESS);
                else
                    ShowMessage("Success", "added the medication '" + name + "'...", true, MessageType.SUCCESS);
            }
            catch (Exception ex) /// Catch exception in case problem occurred
            {
                ShowMessage("Oops!", "an error occurred whilst trying to add this medication...", false, MessageType.ERROR);
                throw ex;
            }
        }


        /// <summary>
        /// Loads the providers into the select_provider dropdown.
        /// </summary>
        public void LoadProviders()
        {
            ///Gets all the information about the providers if it is loaded for the first time
            if (!IsPostBack && select_provider.DataSource == null)
            {
                select_provider.DataSource = DB.ProvidersGet();
                select_provider.DataTextField = "Name";
                select_provider.DataValueField = "Id";
                select_provider.DataBind();
            }
            if (!IsPostBack)
            {
                /// If is editing but doesn't have provider id then all elements are listed
                if (IsEditing && Request.QueryString["providerid"] == null)
                {
                    ListItem itm = select_provider.Items.FindByValue(EditingObject.ProviderID.ToString());
                    if (itm != null)
                        select_provider.SelectedIndex = select_provider.Items.IndexOf(itm);
                }
                /// If provider ID is known takes the d and creates the item list form that instead
                if (Request.QueryString["providerid"] != null)
                {
                    int provider_id = DataLayer.Validator.IsIntegerCorrect(Request.QueryString["providerid"], -1);
                    if (provider_id > -1)
                    {
                        ListItem itm = select_provider.Items.FindByValue(provider_id.ToString());
                        if (itm != null)
                            select_provider.SelectedIndex = select_provider.Items.IndexOf(itm);
                    }
                }
            }
        }

        /// <summary>
        /// Method that runs when continue is clicked this action adds the medication
        /// </summary>
        /// <param name="sender">Specifies the object</param>
        /// <param name="e">specifies the event that occurred</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            AddMedicine();
        }

        /// <summary>
        /// Method that adds the provider return after the button is clicked
        /// </summary>
        /// <param name="sender">Specifies the object</param>
        /// <param name="e">Specifies the event that occurred</param>
        protected void btnAddProviderReturn_Click(object sender, EventArgs e)
        {
            /// URL set to the creation of the provider
            String URL = DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_PROVIDER_INFO);

            ///Validates all the inputs are correct
            String name = txtMedicineName.Text;
            int max_dosage_per_week = DataLayer.Validator.IsIntegerCorrect(txtMaxDosagePerWeek.Text, 2), max_dosage_per_day = DataLayer.Validator.IsIntegerCorrect(txtMaxDosagePerDay.Text, 3);
            int provider_id = DataLayer.Validator.IsIntegerCorrect(select_provider.SelectedValue, -1);

            ///In case of a problem set the previously saved elements and overwrite any changes made
            Session["Medicine_Recovery"] = new BusinessObject.Medication()
            {
                Id = EditingID,
                Date_Added = DateTime.Now,
                Staff = DB.StaffGet(LoggedInUser),
                Max_Dosage_Per_Day = max_dosage_per_day,
                Max_Dosage_Per_Week = max_dosage_per_week,
                Name = name,
                Provider_Info = DB.ProviderGet(provider_id),
                Stock = IsEditing ? DB.StockGet(EditingID) : new BusinessObject.Stock() { Quantity = DataLayer.Validator.IsIntegerCorrect(txtQuantity.Text, 100) }
            };

            ///Redirect the user to creating a provider page
            Redirect(DataLayer.Helper.Pages.CREATE_PROVIDER_INFO, new KeyValuePair<string, string>("return", "1"), (IsEditing ? new KeyValuePair<string, string>("medication", EditingID.ToString()) : new KeyValuePair<string, string>?()));
        }
    }
}