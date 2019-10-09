using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Queue
{
    public partial class Handler : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cmd = Request.QueryString["cmd"] != null ? Request.QueryString["cmd"].ToString().ToLower() : "";
            string args = Request.QueryString["arg"] != null ? Request.QueryString["arg"].ToString() : "";

            if (Request.QueryString["cmd"] == null || Request.QueryString["arg"] == null)
                Response.Redirect(DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.OVERVIEW));


            switch (cmd)
            {
                case "request_patient_queue":
                    int id = DataLayer.Validator.IsIntegerCorrect(args, -1);

                    if (id > -1 && DB.AppointmentCompletionGet(id) != null)
                    {
                        BusinessObject.Appointment_Completion completion = DB.AppointmentCompletionGet(id);
                        completion.Status = 1;
                        DB.Update(completion);
                        DB.SaveChanges();
                        Response.Redirect(string.Format("{0}?id={1}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.VIEW_PROFILE), completion.Appointment.PatientUserID));
                    }
                    break;
                default:
                    Response.Redirect(DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.OVERVIEW));
                    break;
            }

        }
    }
}