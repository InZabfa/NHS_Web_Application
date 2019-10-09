using DataLayer;
using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Patients : BasePage
    {
        public Patients() : base(Permissions.VIEW_PATIENTS) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DB.UserIsStaff(LoggedInUser)) Response.Redirect(DataLayer.Helper.PageAddress(Helper.Pages.OVERVIEW));

            if (!IsPostBack && !hasPatients())
            {
                if (GetRole() != ROLES.ADMIN && GetRole() != ROLES.RECEPTIONIST)
                    ShowMessage("Oops!", "you don't have any patients...", false, MessageType.WARNING);
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (LoggedInUser != null && hasPatients())
            {
                if (!IsPostBack)
                {
                    populateConditions();
                }
            }
        }

        protected void populateConditions()
        {
            foreach (BusinessObject.Condition condition in DB.ConditionsGet())
            {
                tags_searchbox.Items.Add(new ListItem(condition.Name, condition.Id.ToString(), true));
            }
        }

        protected bool hasPatients()
        {
            if (LoggedInUser != null)
            {
                if (DB.UserIsStaff(LoggedInUser) && GetRole() != ROLES.RECEPTIONIST && GetRole() != ROLES.ADMIN)
                {
                    BusinessObject.Staff user = DB.StaffGet(LoggedInUser);
                    if (user.Patients.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}