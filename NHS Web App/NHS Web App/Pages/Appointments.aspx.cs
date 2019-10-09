using DataLayer;
using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Appointments : BasePage
    {
        public Appointments() : base(Permissions.VIEW_APPOINTMENTS) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        protected List<BusinessObject.Appointment> GetTodaysAppointments() => (GetRole() == ROLES.RECEPTIONIST || GetRole() == ROLES.ADMIN ? DB.AppointmentsGet(DateTime.Now) : DB.AppointmentsGet(DB.StaffGet(LoggedInUser), DateTime.Now)).OrderBy(u => u.Appointment_DateTime).ToList();
        protected List<BusinessObject.Appointment> GetTomorrowsAppointments() => (GetRole() == ROLES.RECEPTIONIST || GetRole() == ROLES.ADMIN ? DB.AppointmentsGet(DateTime.Today.AddDays(1)) : DB.AppointmentsGet(DB.StaffGet(LoggedInUser), DateTime.Today.AddDays(1))).OrderBy(u => u.Appointment_DateTime).ToList();

        protected List<BusinessObject.Appointment> GetNextWeekAppointments()
        {
            var monday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek).AddDays(7);
            var sunday = DateTime.Now.AddDays(DayOfWeek.Saturday - DateTime.Now.DayOfWeek).AddDays(8);

            return (GetRole() == ROLES.ADMIN || GetRole() == ROLES.RECEPTIONIST ? DB.AppointmentsGet(monday, sunday) : DB.AppointmentsGet(DB.StaffGet(LoggedInUser), monday, sunday)).OrderBy(u => u.Appointment_DateTime).ToList();
        }

        protected List<BusinessObject.Appointment> GetAfterNextWeekAppointments()
        {
            var sunday = DateTime.Now.AddDays(DayOfWeek.Saturday - DateTime.Now.DayOfWeek).AddDays(8);
            var val = (from ap in (GetRole() == ROLES.ADMIN || GetRole() == ROLES.RECEPTIONIST ? DB.AppointmentsGet() : DB.AppointmentsGet(DB.StaffGet(LoggedInUser)))
                       where ap.Appointment_DateTime > sunday
                       select ap).ToList();
            return val;
        }

        protected void LoadAppointments()
        {
            List<BusinessObject.Appointment> today_appointments = GetTodaysAppointments();
            List<BusinessObject.Appointment> tomorrow_appointments = GetTomorrowsAppointments();
            List<BusinessObject.Appointment> nextweek_appointments = GetNextWeekAppointments();
            List<BusinessObject.Appointment> afternextweek_appointments = GetAfterNextWeekAppointments();


            today_appointments.ForEach(item => appointments_today.Controls.Add(ControlHandler.CreateAppointmentControl(item, this, DeleteAppointment)));
            tomorrow_appointments.ForEach(item => appointments_tomorrow.Controls.Add(ControlHandler.CreateAppointmentControl(item, this, DeleteAppointment)));
            nextweek_appointments.ForEach(item => appointments_nextweek.Controls.Add(ControlHandler.CreateAppointmentControl(item, this, DeleteAppointment)));
            afternextweek_appointments.ForEach(item => appointments_afternextweek.Controls.Add(ControlHandler.CreateAppointmentControl(item, this, DeleteAppointment)));
        }



        bool DeleteAppointment(BusinessObject.Appointment ap)
        {
            try
            {
                DB.Remove(ap, ap.Appointment_Completion);
                DB.SaveChanges();
                ShowMessage(msgContainer, "Success", "removed appointment with reference " + ap.Ref_Number + "...", true, MessageType.SUCCESS);
                appointments_today.Controls.Clear();
                appointments_nextweek.Controls.Clear();
                appointments_tomorrow.Controls.Clear();
                LoadAppointments();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}