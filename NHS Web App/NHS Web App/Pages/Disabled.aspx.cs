using DataLayer;
using System;

namespace NHS_Web_App.Pages
{
    public partial class Disabled : BasePage
    {
        public Disabled() : base(false) { }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (LoggedInUser != null)
            {
                if (LoggedInUser.Access_Levels.Access_Enabled && LoggedInUser.Access_Levels.Access_Level > 0)
                {
                    Response.Redirect(Helper.PageAddress(Helper.Pages.OVERVIEW));
                }
            }
            else
                Response.Redirect(Helper.PageAddress(Helper.Pages.LOGIN));
        }
    }
}