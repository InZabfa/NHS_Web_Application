using DataLayer;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Pages.View
{
    /// <summary>
    /// Medicine is derived from BasePage
    /// </summary>
    public partial class Medicine : BasePage
    {
        /// <summary>
        /// If the persimmision of the user allows them to view medicines, they will be able to do so.
        /// </summary>
        public Medicine() : base(Permissions.VIEW_MEDICINES) { }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}