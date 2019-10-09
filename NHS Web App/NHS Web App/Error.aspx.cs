using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Error : System.Web.UI.Page
    {
        public String ErrorTitle = GlobalVariables.ERROR_PAGETITLE;
        public String Description = GlobalVariables.MESSAGE_PAGENOTFOUND;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Server.GetLastError() != null)
            {
                if (Request.QueryString["result"] != null)
                {
                    switch (Request.QueryString["result"])
                    {
                        case "404":
                            Description = GlobalVariables.MESSAGE_PAGENOTFOUND;
                            break;
                        case "401":
                            Description = GlobalVariables.MESSAGE_NOTAUTHORISED;
                            break;
                        default:
                            Response.Redirect(Helper.PageAddress(Helper.Pages.OVERVIEW));
                            break;
                    }
                }
            }
            else
            {
                Response.Redirect(Helper.PageAddress(Helper.Pages.OVERVIEW));
            }
        }
    }
}