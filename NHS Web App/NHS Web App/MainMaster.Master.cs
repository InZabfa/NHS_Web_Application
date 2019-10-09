using DataLayer;
using System;

namespace NHS_Web_App
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        public BasePage BasePage()
        {
            return Page as BasePage;
        }

        public void SetSearchText(string val)
        {
            search_box.Text = val;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = GlobalVariables.COOKIE_TIMEOUT;
            searchbox.Style.Add("display", (BasePage().HasPermission(NHS_Web_App.BasePage.Permissions.VIEW_MEDICINES) || BasePage().HasPermission(NHS_Web_App.BasePage.Permissions.VIEW_PATIENTS) ||
                                                              BasePage().HasPermission(NHS_Web_App.BasePage.Permissions.VIEW_APPOINTMENTS) ||
                                                              BasePage().HasPermission(NHS_Web_App.BasePage.Permissions.VIEW_STAFF)) ? "block" : "none");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (BasePage().IsSearchEventHandlerAdded())
            {
                BasePage().Search(search_box.Text);
            }
            else
            {
                Response.Redirect("~/Pages/Search.aspx?q=" + search_box.Text);
            }
        }
    }
}