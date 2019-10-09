using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Reference : BasePage
    {
        public Reference() : base(false) { }

        public BusinessObject.Appointment AppointmentFromRefNumber
        {
            get
            {
                if (Request.QueryString["ref"] != null)
                {
                    string @ref = Request.QueryString["ref"].ToString();
                    BusinessObject.Appointment ap = DB.AppointmentGet(@ref);
                    return ap;
                }
                else return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppointmentFromRefNumber == null)
                Response.Redirect(DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.LOGIN));
            else
                ShowMessage(msg_box, "Information", string.Format("Because you are not logged in, you must inform reception you are here...", AppointmentFromRefNumber.Patient.User.Practice_Info.Name), false, MessageType.NONE);
        }
    }
}