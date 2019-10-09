using DataLayer;
using NHS_Web_App.Controls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Pages
{
    public partial class Search : BasePage
    {
        public string SearchTerm { get; set; }

        public Search() : base(MedicalPermissions()) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            OnPassSearch += Search_OnPassSearch;

            if (Request.QueryString["q"] != null)
            {
                SearchTerm = Request.QueryString["q"].ToString();
                if (!IsPostBack) ((MainMaster)Master).SetSearchText(SearchTerm);
            }

            HandleSearch();
        }

        public SearchType GetSearchType
        {
            get
            {
                try
                {
                    if (Request.QueryString["type"] != null) return (SearchType)Enum.Parse(typeof(SearchType), Request.QueryString["type"].ToString(), true);
                }
                catch
                {
                    return SearchType.ALL;
                }
                return SearchType.ALL;
            }
        }

        public enum SearchType
        {
            PATIENTS,
            MEDICINES,
            APPOINTMENTS,
            MEDICATIONS,
            CONDITIONS,
            STAFF,
            ALL
        }

        /// <summary>
        /// Checks all strings in the string[] obj for any value that matches from object[] q.
        /// If an object is string[], that is also checked.
        /// </summary>
        /// <param name="obj">strings to compare</param>
        /// <param name="q">objects to check and compare</param>
        /// <returns>true if contains any, false if not</returns>
        protected bool Compare(string[] obj, params object[] q)
        {
            return obj.Any(z => (q
            .Any(p => p.GetType() == typeof(string[]) ?
            ((string[])p).Any(x => x.ToString().ToLower().Contains(z.ToString().ToLower()))
            : z.ToString().ToLower().Contains(p.ToString().ToLower()))));
        }

        /// <summary>
        /// Clears the contents of all controls passed through as a parameter
        /// </summary>
        /// <param name="controls">Controls to clear</param>
        protected void Clear(params HtmlGenericControl[] controls) => controls.ToList().ForEach(item => item.Controls.Clear());

        protected void HandleSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                String[] ste = SearchTerm.ToLower().Split(' ');

                Clear(appointments, patients, medicines, conditions, staff);

                List<BusinessObject.User> Users = DB.PatientsGetAll()
                    .Select(u => u.User)
                    .Where(user => Compare(ste, user.Forename, user.Surname, user.Phone_number, user.Email, user.Address, user.DOB, user.Phone_number,
                    user.Patient_Notes.Select(u => u.Note)))
                    .ToList();

                List<BusinessObject.Appointment> Appointments = ((GetRole() == ROLES.RECEPTIONIST || GetRole() == ROLES.ADMIN) ? DB.AppointmentsGet() : DB.AppointmentsGet(DB.StaffGet(LoggedInUser)))
                    .Where(ap => Compare(ste, ap.Appointment_DateTime,
                    ap.Ref_Number, ap.Patient.User.Forename,
                    ap.Patient.User.Surname, ap.Patient.User.Email, ap.Patient.User.Phone_number,
                    ap.Patient.User.Address, ap.Patient.User.DOB.ToString(),
                    ap.Patient.User.Patient_Notes.Select(u => u.Note)))
                    .ToList();

                List<BusinessObject.Medication> Medications = DB.MedicationsGet()
                    .Where(m => (Compare(ste, m.Name, m.Provider_Info.Name, m.Provider_Info.Phone_Number, m.Provider_Info.Email_Address, m.Provider_Info.Address)))
                    .ToList();

                List<BusinessObject.Condition> Conditions = DB.ConditionsGet()
                    .Where(co => Compare(ste, co.Additional_Info, co.Name, co.Date_Added)).ToList();

                List<BusinessObject.Staff> Staffs = DB.StaffGetAll()
                     .Where(user => Compare(ste, user.User.Forename, user.User.Surname, user.User.Phone_number, user.User.Email, user.User.Address, user.User.DOB, user.User.Phone_number))
                     .ToList();

                foreach (var item in Users)
                {
                    ControlHandler.Builder builder = new ControlHandler.Builder(item.Surname + ", " + item.Forename, this);
                    builder.AddProperty("Email", @"<a href=""mailto:{0}"">{0}</a>", item.Email);
                    builder.AddProperty("Phone number", @"<a href=""tel:{0}"">{0}</a>", item.Phone_number);
                    builder.AddProperty("Date of birth", item.DOB.ToShortDateString());
                    builder.AddHtml("<hr>");
                    builder.AddProperty("Address", item.Address);
                    if (HasPermission(Permissions.VIEW_PATIENTS))
                    {
                        builder.AddControl(new HyperLink() { NavigateUrl = "/Pages/View/Profile.aspx?id=" + item.Id, Text = "View profile..." });
                    }
                    patients.Controls.Add(builder.ToObject());
                }

                foreach (var item in Medications)
                {
                    ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("{0} ({1}) - {2} in stock", item.Name, item.Provider_Info.Name, item.Stock.Quantity), this);
                    builder.AddProperty("Provider", item.Provider_Info.Name);
                    builder.AddProperty("Provider address", item.Provider_Info.Address);
                    builder.AddProperty("Provider email", @"<a href=""mailto:{0}"">{0}</a>", item.Provider_Info.Email_Address);
                    builder.AddHtml("<hr>");
                    builder.AddProperty("Max dosage per day", item.Max_Dosage_Per_Day.ToString());
                    builder.AddProperty("Max dosage per week", item.Max_Dosage_Per_Week.ToString());

                    if (HasPermission(Permissions.MODIFY_MEDICINE))
                    {
                        Button btnRemove = new Button() { Text = "Remove...", CssClass = "button", Page = this };
                        HyperLink btnEdit = new HyperLink() { Text = "Edit...", CssClass = "button", Page = this, NavigateUrl = string.Format("{0}?edit={1}&return_url={2}", Helper.PageAddress(Helper.Pages.CREATE_MEDICINE), item.Id, Request.Url.LocalPath) };
                        btnRemove.Click += (s, e) => DB.RemoveSave(item);
                        builder.AddControl(btnRemove, btnEdit);
                    }
                    medicines.Controls.Add(builder.ToObject());
                }

                foreach (var item in Appointments)
                {
                    ControlHandler.Builder builder = new ControlHandler.Builder(
                        string.Format("{0} at {1} with <strong>{2}, {3}</strong>", item.Appointment_DateTime.ToShortDateString(),
                        item.Appointment_DateTime.ToShortTimeString(), item.Patient.User.Surname, item.Patient.User.Forename), this);
                    builder.AddHtml("<strong>Patient details...</strong>");
                    builder.AddHtml("<hr>");
                    builder.AddProperty("Name", "{0}, {1}", item.Patient.User.Forename, item.Patient.User.Surname);
                    builder.AddProperty("Email", @"<a href=""mailto:{0}"">{0}</a>", item.Patient.User.Email);

                    if (HasPermission(Permissions.VIEW_PATIENTS))
                    {
                        HyperLink link = new HyperLink() { Text = "View profile...", NavigateUrl = "/Pages/View/Profile.aspx?id=" + item.PatientUserID };
                        builder.AddControl(link);
                    }
                    appointments.Controls.Add(builder.ToObject());
                }

                foreach (var item in Staffs)
                {
                    ControlHandler.Builder builder = new ControlHandler.Builder(item.User.Surname + ", " + item.User.Forename, this);
                    builder.AddProperty("Email", @"<a href=""mailto:{0}"">{0}</a>", item.User.Email);
                    builder.AddProperty("Phone number", @"<a href=""tel:{0}"">{0}</a>", item.User.Phone_number);
                    builder.AddHtml("<hr>");
                    builder.AddProperty("Address", item.User.Address);
                    staff.Controls.Add(builder.ToObject());
                }


                if (!Users.Any()) patients.Controls.Add(new Label() { Text = "No patients found..." });
                if (!Medications.Any()) medicines.Controls.Add(new Label() { Text = "No medicines found..." });
                if (!Appointments.Any()) appointments.Controls.Add(new Label() { Text = "No appointments found..." });

                if (!Staffs.Any()) staff.Controls.Add(new Label() { Text = "No staff found..." });
                if (!conditions.HasControls()) conditions.Controls.Add(new Label() { Text = "No conditions found..." });
            }
        }

        private void Search_OnPassSearch(string search_term)
        {
            Response.Redirect("~/Pages/Search.aspx?q=" + search_term);
        }
    }
}