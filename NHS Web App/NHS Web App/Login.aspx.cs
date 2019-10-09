using BusinessObject;
using DataLayer;
using System;
using System.IO;

namespace NHS_Web_App
{
    public partial class Login : BasePage
    {
        public Login() : base(false) { }

        public String Return_URL { get; set; }

        /// <summary>
        /// Upon page load, if the URL has the query string of '?error=1' 
        /// then show error message.
        /// </summary>
        /// <param name="sender">The object from which the event was fired from.</param>
        /// <param name="e">EventArgs param</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Return_URL = string.Empty;
            if (!DB.HasAdmin())
            {
                Response.Redirect(Helper.PageAddress(Helper.Pages.WELCOME));
            }
            else
            {
                if (LoggedInUser != null)
                {
                    Response.Redirect(GetRole() == ROLES.RECEPTIONIST ? Helper.PageAddress(Helper.Pages.APPOINTMENTS) : Helper.PageAddress(Helper.Pages.OVERVIEW));
                }
                else
                {
                    if (Request.QueryString["error"] != null)
                    {
                        if (Request.QueryString["error"] == "1")
                        {
                            ShowMessage(error_message, "Incorrect login", "your login details are incorrect, try again!", false, MessageType.ERROR);
                        }
                    }

                    if (Request.QueryString["return"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["return"]))
                    {
                        try
                        {
                            string return_url = Request.QueryString["return"];
                            if (Server.MapPath(return_url) != string.Empty && File.Exists(Server.MapPath(return_url)))
                            {
                                Return_URL = return_url;
                            }
                        }
                        catch (Exception)
                        {
                            Return_URL = string.Empty;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Upon login press, retrieve entered Email and Password text.
        /// Retrieve the user from the database with the entered email.
        /// If User is null, show login error.
        /// If the User exists, then compare passwords, set LoggedInUser to the retrieved user.
        /// Redirect to Default page.
        /// </summary>
        /// <param name="sender">The object from which the event was fired from.</param>
        /// <param name="e">EventArgs param</param>
        protected void Login_Click(object sender, EventArgs e)
        {
            String email = txtEmail.Text;
            String pwrd = txtPassword.Text;
            User user = Helper.Login(email, pwrd, DB);
            if (user != null)
            {
                Response.Write("Password: " + pwrd);
                LoggedInUser = user;
                if (!string.IsNullOrWhiteSpace(Return_URL))
                {
                    Response.Redirect(Return_URL);
                }
                else
                    Response.Redirect("Default.aspx");
            }
            ShowMessage(error_message, "Incorrect login", "your login details are incorrect, try again!", false, MessageType.ERROR);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Appointment ap = DB.AppointmentGet(txtReference.Text);
            if (ap != null) Response.Redirect(string.Format("~/Reference.aspx?ref={0}", txtReference.Text));
            else ShowMessage(error_message, "Oops!", "this reference could not be found, try again...", false, MessageType.ERROR);
        }
    }
}