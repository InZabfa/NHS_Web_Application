using DataLayer;
using BusinessObject;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NHS_Web_App.Handlers
{
    public class QueueHandler
    {
        private Repository _repository;

        public Repository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

        /// <summary>
        /// Returns flaggable appointments - any appointment the user can flag the status as arrived (appointments due within an hour);
        /// </summary>
        /// <returns></returns>
        public Appointment GetLoggedInPatientAppointmentsFlaggable(BusinessObject.Patient pa)
        {
            if (pa != null)
            {
                foreach (var item in pa.Appointments)
                {
                    DateTime _min = item.Appointment_DateTime.AddMinutes(-60);
                    if (DateTime.Now >= _min && DateTime.Now <= item.Appointment_DateTime && item.Appointment_Completion.Status < 0) return item;
                }
            }
            return null;
        }

        public Appointment_Completion GetCurrentAppointment(BusinessObject.Patient pa)
        {
            var query = (from i1 in pa.Appointments where i1.Appointment_DateTime >= DateTime.Now && i1.Appointment_DateTime.AddMinutes(i1.Appointment_Duration_Minutes) > DateTime.Now && i1.Appointment_Completion.Status < 3 select i1).FirstOrDefault();
            if (query != null) return query.Appointment_Completion; else return null;
        }

        /// <summary>
        /// QueueHandler Constructor
        /// </summary>
        /// <param name="repository">Repository object to use within methods</param>
        public QueueHandler(Repository repository) => Repository = repository;


        public List<Appointment_Completion> GetTodayAppointments(BusinessObject.Staff staff)
        {
            List<Appointment_Completion> appointments = Repository.AppointmentsGet(staff, DateTime.Today)
                .Where(a => a.Appointment_Completion != null && a.Appointment_Completion.Status == 0)
                .Select(u => u.Appointment_Completion)
                .OrderBy(p => p.Appointment.Appointment_DateTime)
                .ToList();
            return appointments;
        }

        public void RequestPatient(Appointment_Completion app)
        {

        }

        public bool HasWaitingPatients(BusinessObject.Staff staff) => GetTodayAppointments(staff).Count > 0;


        /// <summary>
        /// Returns the patients ready to be seen.
        /// </summary>
        /// <returns>List of patients ready to be seen</returns>
        public List<Appointment_Completion> GetCurrentPatients(BusinessObject.Staff staff)
        {
            List<Appointment_Completion> appointments = Repository.AppointmentsGet(staff, DateTime.Today)
               .Where(a => a.Appointment_Completion != null && a.Appointment_Completion.Status == 1)
               .Select(u => u.Appointment_Completion)
               .ToList();
            return appointments;
        }
    }
}