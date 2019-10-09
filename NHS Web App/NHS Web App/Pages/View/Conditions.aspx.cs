using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Pages.View
{
    /// <summary>
    /// Conditions is derived from BasePage
    /// </summary>
    public partial class Conditions : BasePage
    {
        public Conditions() : base(MedicalPermissions()) { }

        /// <summary>
        /// This method creates the page for the coniditions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Handlers.Handler.HandleAuthorisation(DB, LoggedInUser, Response, DataLayer.GlobalVariables.ACCESS_LEVELS.DOCTOR, Request.Url.LocalPath);
            OnPassSearch += Conditions_OnPassSearch;
        }


        private void Conditions_OnPassSearch(string search_term)
        {
           
        }
    }
}