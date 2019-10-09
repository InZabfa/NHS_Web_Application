using DataLayer;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// This class is responsible for the creation and manipulation of the appointments that are created.
/// </summary>

namespace NHS_Web_App.Pages.Create
{
    /// <summary>
    /// Appointment is derived from BasePage.
    /// </summary>
    public partial class Appointment : BasePage
    {

        public Appointment() : base(Permissions.VIEW_APPOINTMENTS) { }

        /// <summary>
        /// This method is responsible for getting and setting the appointment reference. 
        /// </summary>
        public string GetAppointmentReference
        {
            get
            {
                if (ViewState["AppointmentReference"] != null)
                    return ViewState["AppointmentReference"].ToString();
                return null;
            }
            set
            {
                ViewState["AppointmentReference"] = value;
            }
        }

        /// <summary>
        /// Allows the user to get doctor from a drop-down menu.
        /// </summary>
        public BusinessObject.Staff GetSelectedDoctor
        {
            get
            {
                return DB.StaffGet(Validator.IsIntegerCorrect(doctor_picker.SelectedValue.ToString(), -1));
            }
        }

        /// <summary>
        /// Allows the user to select the patient from a drop-down menu.
        /// </summary>
        public BusinessObject.Patient GetSelectedPatient
        {
            get
            {
                return DB.PatientGet(Validator.IsIntegerCorrect(select_patients.SelectedValue.ToString(), -1));
            }
        }

        /// <summary>
        /// Populates the dropdown menu for doctors, this is only done when loaded for the first time and not after postback
        /// </summary>
        protected void loadDoctors()
        {
            if (!IsPostBack)
            {
                doctor_picker.Items.Clear();///Clears the elements currently in the dropdown

                ///Goes through every staff member in database and presents them in the format specified 
                foreach (var staff in DB.StaffGetAll().Where(i => i.Id == i.Id && i.Staff_Role == "DOCTOR"))
                {
                    doctor_picker.Items.Add(new ListItem(string.Format("{0}, {1} ({2})",
                        staff.User.Surname, staff.User.Forename, staff.User.Email),
                        staff.Id.ToString()));
                }
            }
            ///if patient is selected then the button to continue is enabled
            btnContinue.Enabled = select_patients.Items.Count > 0;
        }

        /// <summary>
        /// This method runs when the page is first loaded. It ensures that appointments
        /// are successfully created for patients and staff and prints a message to confirm it. It also makes sure that appointments
        /// are booked in the appropriate free time slots based on the working hours.
        /// </summary>
        /// <param name="sender">Specifies the object</param>
        /// <param name="e">Specifies the event that occurred</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoggedInUser != null && DB.StaffGet(LoggedInUser) != null) { loadDoctors(); if (!IsPostBack) loadPatients(); } /// makes sure the current user is logged in

            if (!IsPostBack)
            {
                GetAppointmentReference = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
                refNumber.Text = GetAppointmentReference; ///creates a reference number for the appointment
            }

            ///If all the requests are correct, user is logged in and the access is correct then the appointment is created and message is printed
            if (Request.QueryString["success"] != null)
                if (Validator.IsIntegerCorrect(Request.QueryString["success"].ToString(), -1) == 1)
                    if (Request.QueryString["ref"] != null)
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["ref"].ToString()))
                        {
                            BusinessObject.Appointment app = DB.AppointmentGet(Request.QueryString["ref"].ToString());
                            if (app != null)
                                if (app.Staff.UserID == LoggedInUser.Id || GetRole() == ROLES.ADMIN || GetRole() == ROLES.RECEPTIONIST)
                                    ShowMessage("Success", "appointment created for " + app.Patient.User.Forename + " on " + app.Appointment_DateTime.ToString("dd/MM/yyyy") + " at " + app.Appointment_DateTime.ToString("HH:mm") + "...", true, MessageType.SUCCESS);
                        }

            if (!IsPostBack)
            {
                DateTime cT = DateTime.Now;
                if (DateTime.Now.Hour < GlobalVariables.START_WORK_HOUR) /// Checks if the current hour is smaller than the hour that a shift starts.
                    txtStartTime.Text = new DateTime(cT.Year, cT.Month, cT.Day, GlobalVariables.START_WORK_HOUR, 0, 0).ToString("HH:mm");
                else if (DateTime.Now.Hour == GlobalVariables.START_WORK_HOUR)// Checks is the current hour is equal to the hour that a shift starts.
                    txtStartTime.Text = new DateTime(cT.Year, cT.Month, cT.Day, cT.Hour, cT.Minute, 0).ToString("HH:mm");
                else
                {
                    if (DateTime.Now.Hour <= GlobalVariables.END_WORK_HOUR) /// Checks if the current hour is smaller or equal to the hour that a shift starts.
                        if (GetAvailableSlots()[0] != null)
                            txtStartTime.Text = GetAvailableSlots()[0].Start.ToString("HH:mm");
                }

                if (DateTime.Now.Hour >= GlobalVariables.END_WORK_HOUR) txtDate.Text = cT.AddDays(1).ToString("yyyy-MM-dd");
                else txtDate.Text = cT.ToString("yyyy-MM-dd");

                /// If a cookie exists for roomNumber, set txtRoom to roomNumber value
                txtRoom.Text = (Request.Cookies["roomNumber"]?.Value ?? "") ?? "";
                chkRememberRoom.Checked = (Request.Cookies["roomNumber"] != null);

                /// If a cookie exists for appointmentDuration, set txtDuration to value (if parse successful).
                txtDuration.Text = Request.Cookies["appointmentDuration"] != null ? Validator.IsIntegerCorrect((Request.Cookies["appointmentDuration"]?.Value ?? "15") ?? "15").ToString() : "15";
                chkRememberTime.Checked = (Request.Cookies["appointmentDuration"] != null);
            }
            btnContinue.Enabled = HasPatients() && HasDoctors() && ValidTimeDate(); /// all elements are complete then appointment can be created
            DataBindChildren(); /// Binds data together
        }

        public TimePeriodCollection GetAvailableSlots() => getAvailableSlots(GlobalVariables.START_WORK_HOUR, GlobalVariables.END_WORK_HOUR, Validator.IsDateTimeCorrect(txtDate.Text), Validator.IsIntegerCorrect(txtDuration.Text));
        protected bool HasPatients() => select_patients.Items.Count > 0; /// Select a patient from a drop down list.
        protected bool HasDoctors() => doctor_picker.Items.Count > 0; /// Select a doctor from a drop down list.
        protected bool ValidTimeDate()
        {
            DateTime appointment_date = Validator.IsDateTimeCorrect(txtDate.Text, DateTime.Now); /// Validates if the date is correct for an appointment.
            DateTime appointment_time = Validator.IsDateTimeCorrect(txtStartTime.Text, DateTime.Now); /// Validates if the time is correct for an appointment.
            DateTime obj = new DateTime(appointment_date.Year, appointment_date.Month, appointment_date.Day, appointment_time.Hour, appointment_time.Minute, 0); /// Initialises a new of DateTime
            return obj > DateTime.Now;

        }
        /// A method that loads the details on the patients.
        protected void loadPatients()
        {
            select_patients.Items.Clear(); /// Clears the patients details from list item.
            if (GetSelectedDoctor != null)
            {
                /// For each doctor add the details of the patient stored in the format specified.
                DB.PatientsGetAll().ForEach(patient => select_patients.Items.Add(new ListItem(string.Format("{0}, {1} ({2})",
                    patient.User.Surname, patient.User.Forename, patient.User.Email),
                    patient.UserID.ToString())));
            }
            btnContinue.Enabled = select_patients.Items.Count > 0 && doctor_picker.Items.Count > 0;
        }


        protected BusinessObject.Appointment GetAppointmentObject(BusinessObject.Staff staff_user) /// Declaring local appointments information variables to the input
        {
            DateTime appointment_date = Validator.IsDateTimeCorrect(txtDate.Text); /// Checks is the appointment date is correct format.
            DateTime appointment_time = Validator.IsDateTimeCorrect(txtStartTime.Text); /// Checks if the appointment time is in the correct format.
                                                                                        /// Initializes a new instance of DateTime.
            DateTime time_date = new DateTime(appointment_date.Year, appointment_date.Month, appointment_date.Day, appointment_time.Hour, appointment_time.Minute, 0);
            /// Sets the columns of the database to the local elements to make sure they are stored correctly
            BusinessObject.Appointment appointment = new BusinessObject.Appointment()
            {
                Appointment_DateTime = time_date,
                Appointment_Duration_Minutes = Validator.IsIntegerCorrect(txtDuration.Text), /// Checks if the duration of the appointment is acceptable, if not set by default its 15 minutes.
                Patient = GetSelectedPatient,
                Ref_Number = GetAppointmentReference,
                Room = txtRoom.Text ?? "Na",
                Staff = staff_user ?? null, /// check if staff is null 

                /// the appointment status is then stored in the database in the separate table
                Appointment_Completion = new BusinessObject.Appointment_Completion()
                {
                    Changed_On = DateTime.Now,
                    Staff = staff_user ?? null,
                    Status = -1
                }
            };
            return appointment;
        }

        /// <summary>
        /// This method is running when an action occurs, in this case the button to save appointment is clicked
        /// </summary>
        /// <param name="sender">name of the object</param>
        /// <param name="e">Ensures even occurs</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                handleCookies();

                BusinessObject.Patient patient = GetSelectedPatient; /// Selects the required patient.
                BusinessObject.Staff staff_user = GetSelectedDoctor; /// Selects the required staff member that currently is the user.

                BusinessObject.Appointment appointment = GetAppointmentObject(staff_user); /// Gets the appoints using staff memeber.

                ///Performs all the checks to make sure that the requested creating of the appointment is not overlapping any of the other already existent appointments
                List<BusinessObject.Appointment> appointments = (from ap1 in staff_user.Appointments where ap1.Appointment_DateTime.Date == Validator.IsDateTimeCorrect(txtDate.Text) select ap1).Where(t1 => new TimeRange(
                    t1.Appointment_DateTime, t1.Appointment_DateTime.AddMinutes(t1.Appointment_Duration_Minutes)).OverlapsWith
                    (new TimeRange() { Start = appointment.Appointment_DateTime, End = appointment.Appointment_DateTime.AddMinutes(Validator.IsIntegerCorrect(txtDuration.Text)) })).ToList();

                /// Checks if appoint is being booked in free space or it prints an error message if an appointment is scheduled where another appointment has been allocated.
                if (appointments.Count > 0)
                {
                    CloseMessages();
                    ShowMessage("Oops!", "this appointment collides with other appointments...", false, MessageType.ERROR);
                    return;
                }
                /// Checks if appointment is not being booked in the past or it prints an error message saying it must be in present tense.
                if (appointment.Appointment_DateTime <= DateTime.Now)
                {
                    CloseMessages();
                    ShowMessage("Oops!", "appointments must be in the present tense...", false, MessageType.ERROR);
                    return;
                }
                /// This runs if an appointment has a reference number then new reference number is created.
                if (DB.AppointmentGet(GetAppointmentReference) != null)
                {
                    string new_ref = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
                    GetAppointmentReference = new_ref;
                    refNumber.Text = new_ref;
                    appointment.Ref_Number = new_ref;
                    CloseMessages();
                    ShowMessage("Info", "this appointment reference number has changed to " + new_ref + "...", false, MessageType.INFORM);
                }

                DB.AppointmentsAdd(appointment); /// Adds an appointment to the appointment table.
                if (GetSelectedPatient.Email_Notifications)
                {
                    Helper.Email(LoggedInUser.Practice_Info.Email, GetSelectedPatient.User.Email, "The appointment has been created",
                        String.Format("<p>Appointment is on : {0}</p><hr /><br /><p>Your appointment will be: {1}</p><hr/><br/><p>The appointment Reference number is: {2}</p><hr/><br/> <p> In room: {3}", appointment.Appointment_DateTime, appointment.Appointment_Duration_Minutes, appointment.Ref_Number, appointment.Room));
                }
                DB.SaveChanges(); /// Saves the changes in the database.

                string app_ref = DB.AppointmentGet(appointment.Ref_Number).Ref_Number; /// Creates a string variable for appointment reference number.
                Response.Redirect(Helper.PageAddress(Helper.Pages.CREATE_APPOINTMENT) + "?success=1&ref=" + app_ref); /// Redirects the user to a new URL to create an appointment.
            }
            catch /// Catches errors if appointments cannot be created.
            {
                CloseMessages();
                ShowMessage("Oops!", "could not create the appointment...", false, MessageType.ERROR);
            }
        }

        /// <summary>
        /// The method handles all the cookie related problems
        /// </summary>
        protected void handleCookies()
        {
            if (chkRememberTime.Checked) /// Remember time is a check box, this statement is correct if remember time has been checked.
            {
                HttpCookie cookie = Request.Cookies["appointmentDuration"]; /// Gets the HTTP request object for the requested page.

                if (cookie != null) /// If cookies it not equal to null, the if statement below will run which will set the Value of cookie to the correct integer for duration.
                    if (cookie.Value != Validator.IsIntegerCorrect(txtDuration.Text, 15).ToString()) /// Checks if the value of cookie is not equal to the duration (time).
                    {
                        Response.Cookies["appointmentDuration"].Value = Validator.IsIntegerCorrect(txtDuration.Text, 15).ToString();
                        Response.Cookies["appointmentDuration"].Expires = DateTime.Now.AddYears(1);
                    }
            }
            else /// This runs if the remember time box isn't checked.
            {
                if (Response.Cookies["appointmentDuration"] != null)
                    Response.Cookies["appointmentDuration"].Expires = DateTime.Now.AddDays(-1);
            }

            if (chkRememberRoom.Checked) /// Remember room is a check box, this statement is correct if remember room has been checked.
            {
                HttpCookie cookie = Response.Cookies["roomNumber"]; /// Gets the HTTP request object for the requested page.
                if (cookie != null) /// If cookie it not equal to null, the if statement below will run which will set the Value of cookie to the correct room.
                    if (cookie.Value != txtRoom.Text)
                    {
                        Response.Cookies["roomNumber"].Value = txtRoom.Text;
                        Response.Cookies["roomNumber"].Expires = DateTime.Now.AddYears(1);
                    }
            }
            else /// This runs if the remember room box isnt checked.
            {
                if (Response.Cookies["roomNumber"] != null)
                    Response.Cookies["roomNumber"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// This method shows all the overlapping times for appointments.
        public bool overlapsExisting(TimePeriodCollection times, TimeRange time)
        {
            foreach (var item in times)
                if (times.GetRelation(time) == PeriodRelation.Inside ||
                    times.GetRelation(time) == PeriodRelation.EndInside ||
                    times.GetRelation(time) == PeriodRelation.InsideEndTouching ||
                    times.GetRelation(time) == PeriodRelation.ExactMatch ||
                    times.GetRelation(time) == PeriodRelation.EnclosingEndTouching ||
                    times.GetRelation(time) == PeriodRelation.Enclosing ||
                    times.GetRelation(time) == PeriodRelation.EnclosingStartTouching ||
                    times.GetRelation(time) == PeriodRelation.InsideStartTouching ||
                    times.GetRelation(time) == PeriodRelation.StartInside) return true;
            return false;
        }

        /// <summary>
        /// Method that checked for all the overlapping times
        /// </summary>
        /// <param name="date_to_check">Takes the date chosen</param>
        /// <param name="test_date_time">Takes the time chosen</param>
        /// <returns>true or false depending on whether it overlaps or not</returns>
        public bool overlapsExisting(DateTime date_to_check, DateTime test_date_time)
        {
            return (from ap1 in DB.StaffGet(LoggedInUser).Appointments where ap1.Appointment_DateTime.Date == date_to_check select ap1).Where(t1 => new TimeRange(t1.Appointment_DateTime, t1.Appointment_DateTime.AddMinutes(t1.Appointment_Duration_Minutes)).OverlapsWith(new TimeRange() { Start = test_date_time, End = test_date_time.AddMinutes(Validator.IsIntegerCorrect(txtDuration.Text)) })).Count() > 0;
        }

        /// <summary>
        /// This method finds the available slots using the starting time, ending hour (when GP closes) and the date.
        /// </summary>
        /// <param name="starting_time">Takes Starting time as an integer</param>
        /// <param name="ending_hour">Takes the Ending time as an integer</param>
        /// <param name="date">Takes the date</param>
        /// <param name="val">value of the duration of the appointment</param>
        /// <returns>The time selected and can't be used any more</returns>

        public TimePeriodCollection getAvailableSlots(int starting_time, int ending_hour, DateTime date, int val)
        {
            if (LoggedInUser != null) /// Ensures that the user is logged in.
                if (DB.StaffGet(LoggedInUser) != null)
                {
                    int duration = val > 0 ? val : 15;
                    /// A new datatime is initialized for the end_datatime and start_datatime.
                    DateTime end_datetime = new DateTime(date.Year, date.Month, date.Day, ending_hour, 0, 0), start_datetime = new DateTime(date.Year, date.Month, date.Day, starting_time, 0, 0);
                    /// TimePeriodCollection is initialized with the structure using start_datatime and end_datatime.
                    TimePeriodCollection freeTimes = new TimePeriodCollection() { Start = start_datetime, End = end_datetime };
                    /// This selects each appointment for the logged in users (logical and comparison is made)
                    /// 

                    /// A loop that assigns the values of start, end and duration to i.
                    for (DateTime i = start_datetime; i < end_datetime; i = i.AddMinutes(duration))
                    {
                        /// Creates a new TimeRange.
                        TimeRange ap = new TimeRange() { Start = i, End = i.AddMinutes(duration), Duration = TimeSpan.FromMinutes(duration) };
                        if (i >= DateTime.Now && ap.Start >= start_datetime && ap.End <= end_datetime && !overlapsExisting(date, i)) { freeTimes.Add(ap); }
                    }
                    freeTimes.SortByStart(ListSortDirection.Ascending); /// Sorts the free times in ascending order.
                    return freeTimes;
                }
            return new TimePeriodCollection();
        }

        protected void doctor_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPatients();
        }
    }
}