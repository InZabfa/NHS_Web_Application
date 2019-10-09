using DataLayer;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Reports : BasePage
    {
        public Reports() : base(Permissions.VIEW_REPORTS) { }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}