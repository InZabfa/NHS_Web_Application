using NHS_Web_App.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Patient
{
    public partial class Default : BasePage
    {
        public Default() : base(Permissions.VIEW_BOOKING) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAppointments();
            apPanel.Update();
        }

        protected new void Page_PreInit()
        {
            base.Page_PreInit();
        }

        public BusinessObject.Appointment FlaggableAppointments
        {
            get
            {
                return QueueHandler.GetLoggedInPatientAppointmentsFlaggable(DB.PatientGet(LoggedInUser.Id));
            }
        }

        protected void LoadAppointments()
        {
            if (LoggedInUser != null)
                foreach (var item in LoggedInUser.Patient.Appointments) { list_appointments.Controls.Add(ControlHandler.CreateAppointmentControl(item, this, true)); }
        }

        protected void btnImhere_Click(object sender, EventArgs e)
        {
            DB.AppointmentCompletionGet(FlaggableAppointments.Id).Status = 0;
            DB.SaveChanges();
            ShowMessage("Success!", "We've informed your doctor. You should appear on the queue.", false, MessageType.SUCCESS);
            apPanel.Update();
        }
    }
}