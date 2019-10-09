using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Logout : BasePage
    {
        public Logout() : base(false) { }


        /// <summary>
        /// Upon page load, set LoggedInUser to null
        /// set LoggedInUserID to -1 and redirect to the login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggedInUser = null;
            LoggedInUserID = -1;
            Response.Redirect("Login.aspx", false);
        }
    }
}