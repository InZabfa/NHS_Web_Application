using System;

/// <summary>
/// Class that allows to add notes to the patient profile
/// </summary>
namespace NHS_Web_App.Pages.Create
{
    public partial class Note : BasePage
    {
        public Note() : base(Permissions.MODIFY_PATIENT_MEDICAL_INFO) { }

        /// <summary>
        /// When page is loaded this method is run
        /// </summary>
        /// <param name="sender">Object created at the page load</param>
        /// <param name="e">Event that occurred</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ///If the page is loaded for the first time and doesn't have any notes
            if (!IsPostBack && GetPatientNotes != null)
            {
                txtNote.Text = GetPatientNotes.Note;///get any notes the patient has
                txtMinAccessLevel.Text = GetPatientNotes.Lowest_Access_Level_Required.ToString(); /// get patient access level
                btnContinue.Text = "Save Changes..."; /// change button to save changes
                txtMinAccessLevel.Text = LoggedInUser.Access_Levels.Access_Level.ToString(); /// get current user access level
            }

            ///If patient has no notes or no patient then take them back to the original page
            if (GetPatientNotes == null && GetUser == null)
            {
                HandleReturn();
            }

            /// if patient doesn't have notes or there are no patients redirect them to the overview page
            if (GetPatientNotes == null)
                if (GetUser == null)
                    Response.Redirect(DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.OVERVIEW));
        }

        /// <summary>
        /// Method that gets the patient notes that already exist in the database
        /// </summary>
        public BusinessObject.Patient_Notes GetPatientNotes
        {
            get
            {
                if (Request.QueryString["edit"] != null) /// not in an editing mode
                {
                    int val = DataLayer.Validator.IsIntegerCorrect(Request.QueryString["edit"].ToString(), -1);
                    if (val >= 0) return DB.GetPatientNote(val);/// return the notes saved
                }
                return null;
            }
        }
        /// <summary>
        /// Gets user from the database that is a patient
        /// </summary>
        public BusinessObject.User GetUser
        {
            get
            {
                if (Request.QueryString["patient"] != null)
                {
                    int val = DataLayer.Validator.IsIntegerCorrect(Request.QueryString["patient"].ToString(), -1);
                    if (val >= 0) return DB.UserGet(val); /// returns the patient
                }
                return null;
            }
        }

        /// <summary>
        /// Method that allows the current user to add notes to the patient
        /// </summary>
        public void AddNote()
        {
            ///Get the database that stores the notes, connect it to the patient id
            BusinessObject.Patient_Notes note = GetPatientNotes != null ? DB.GetPatientNote(GetPatientNotes.Id) : new BusinessObject.Patient_Notes();

            /// if patient has notes allow the update to occur and run the method that allows to update the information provided
            if (GetPatientNotes != null)
            {
                note.Note = txtNote.Text;
                note.Lowest_Access_Level_Required = DataLayer.Validator.IsIntegerCorrect(txtMinAccessLevel.Text, LoggedInUser.Access_Levels.Access_Level);
                DB.Update(note);
            }
            else /// else add a new note with all the information required
            {
                note.User = GetUser;
                note.Staff = DB.StaffGet(LoggedInUser);
                note.Note = txtNote.Text;
                note.Added_DateTime = DateTime.Now;
                note.Lowest_Access_Level_Required = DataLayer.Validator.IsIntegerCorrect(txtMinAccessLevel.Text, LoggedInUser.Access_Levels.Access_Level);
                DB.PatientNoteAdd(note);
            }

            DB.SaveChanges();///save the changes to the database
            HandleReturn();//return to the previous page
            ShowMessage("Success", "added patient note...", true, MessageType.SUCCESS);///show message that the addition was successful
        }
        /// <summary>
        /// Method run if the button continue is clicked, adds the note to the database
        /// </summary>
        /// <param name="sender">Specifies the object</param>
        /// <param name="e">Specifies the event that occurs</param>
        protected void btnContinue_Click(object sender, EventArgs e) => AddNote();
    }
}