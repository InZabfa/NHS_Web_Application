using DataLayer;
using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Default : BasePage
    {
        public Default() : base() { }

        protected void Page_Load()
        {
            if (!DB.UserIsStaff(LoggedInUser))
            {
                Response.Redirect(Helper.PageAddress(Helper.Pages.PATIENT_LANDING));
            }
            else
            {
                if (GetRole() == ROLES.RECEPTIONIST) Response.Redirect(Helper.PageAddress(Helper.Pages.APPOINTMENTS));
                if (DB.UserIsStaff(LoggedInUser) && LoggedInUser.Access_Levels.Access_Level > (int)GlobalVariables.ACCESS_LEVELS.MEDICAL_STAFF)
                    addUsers();
            }
        }
        public void addUsers()
        {
            appointments_list.Controls.Clear();
            foreach (BusinessObject.Appointment ap in (from app in DB.AppointmentsGet(DB.StaffGet(LoggedInUser)) where app.Appointment_DateTime.Year == DateTime.Now.Year && app.Appointment_DateTime.Month == DateTime.Now.Month && app.Appointment_DateTime.Day == DateTime.Now.Day select app))
            {
                ExpandableControl tr = (ExpandableControl)Page.LoadControl("~/Controls/ExpandableControl.ascx");
                tr.Title = String.Format("{1}, {0} - {2} - {3}", ap.Patient.User.Forename, ap.Patient.User.Surname, ap.Appointment_DateTime.ToShortTimeString(), ap.Appointment_DateTime.AddMinutes(ap.Appointment_Duration_Minutes).ToShortTimeString());
                ExpandableControl.InnerControl innerContents = new ExpandableControl.InnerControl();

                HtmlGenericControl ap_room = new HtmlGenericControl("p");
                ap_room.InnerHtml = "<strong>Room:</strong> " + ap.Room;
                innerContents.Controls.Add(ap_room);

                HtmlGenericControl ap_ref = new HtmlGenericControl("p");
                ap_ref.InnerHtml = "<strong>Reference Number:</strong> " + ap.Ref_Number;
                innerContents.Controls.Add(ap_ref);

                ExpandableControl.InnerControl buttonContents = new ExpandableControl.InnerControl();

                HyperLink link = new HyperLink()
                {
                    NavigateUrl = Helper.PageAddress(Helper.Pages.VIEW_PROFILE) + "?id=" + ap.PatientUserID,
                    Text = "View profile..."
                };
                buttonContents.Controls.Add(link);


                tr.Contents = innerContents;
                tr.Buttons = buttonContents;
                tr.IsExpandable = true;
                tr.CollapseAllUponExpanding = true;
                appointments_list.Controls.Add(tr);
            }
        }

        protected void viewPatient(BusinessObject.Appointment_Completion app)
        {
            //DialogControl _dialog = LoadControl("~/Controls/DialogControl.ascx") as DialogControl;

            //DialogControl.InnerControl headerContainer = new DialogControl.InnerControl();
            //headerContainer.Controls.Add(new HtmlGenericControl("h2") { InnerText = string.Format("{0} {1}", app.Appointment.Patient.User.Forename, app.Appointment.Patient.User.Surname) });
            //_dialog.Header = headerContainer;

            //DialogControl.InnerControl contentContainer = new DialogControl.InnerControl();

            //Button btnView = new Button() { Text = "Request patient from queue...", CssClass = "button long", CausesValidation = false };
            //contentContainer.Controls.Add(btnView);
            //_dialog.Body = contentContainer;
            //dialogcontainer.Controls.Add(_dialog);

            throw new NotImplementedException();
        }

        protected void ticketStripTimer_Tick(object sender, EventArgs e)
        {
            if (LoggedInUser != null && DB.UserIsStaff(LoggedInUser))
            {
                if (QueueHandler.HasWaitingPatients(DB.StaffGet(LoggedInUser)))
                {
                    list_tickets.Controls.Clear();

                    foreach (var item in QueueHandler.GetTodayAppointments(DB.StaffGet(LoggedInUser)))
                    {
                        TicketControl control = LoadControl("~/Controls/TicketControl.ascx") as TicketControl;
                        control.Title = string.Format("{0} - {1}", item.Appointment.Appointment_DateTime.ToShortTimeString(), item.Appointment.Appointment_DateTime.AddMinutes(item.Appointment.Appointment_Duration_Minutes).ToShortTimeString());
                        control.Body = string.Format("{0}, {1}", item.Appointment.Patient.User.Surname, item.Appointment.Patient.User.Forename);
                        control.Footer = "Arrived, waiting...";
                        control.TicketType = TicketControl.Type.ready;

                        control.Link = "/Queue/Handler.aspx?cmd=request_patient_queue&arg=" + item.Appointment.Id;

                        list_tickets.Controls.Add(control);
                    }
                }
            }
        }
    }
}
