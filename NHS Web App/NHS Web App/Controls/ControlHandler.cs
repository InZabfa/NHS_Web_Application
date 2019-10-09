using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Its a class to develop quick access to the methods that can be used to create the dropdown information boxes,
/// this will ensure that the format is maintained throughout the website
/// </summary>
namespace NHS_Web_App.Controls
{
    public class ControlHandler
    {
        public struct Builder : IDisposable
        {
            //defines local variables
            private String Title, Footer;
            private StringBuilder sBuilder;
            private Page Page;
            private List<Control> Controls;

            /// <summary>
            /// Produces constructer for the local variables and connects them with the actual html variables
            /// </summary>
            /// <param name="title">The title of the page </param>
            /// <param name="page">The page that it is used for </param>
            /// <param name="FooterHtml">The footer of the page</param>
            public Builder(string title, Page page, String FooterHtml = "")
            {
                Title = title;
                Page = page;
                Footer = FooterHtml;
                sBuilder = new StringBuilder();
                Controls = new List<Control>();
            }
            /// <summary>
            /// Method that formats two strings in the specified format when you pass them
            /// </summary>
            /// <param name="property">The string variable that appers in the strong tags</param>
            /// <param name="val">The string that appears after the strong tags as normal paragraph </param>
            public void AddProperty(string property, string val) => sBuilder.AppendFormat("<p><strong>{0}: </strong>{1}</p>\n", property, val);

            /// <summary>
            /// Method that also formats the property however this time it allows the element of the array (database) to be added 
            /// </summary>
            /// <param name="property">string that appers in the strong tags</param>
            /// <param name="formatString">Allows to use the String.Format to format the values </param>
            /// <param name="values">Allows to format the array element</param>
            public void AddProperty(string property, string formatString, params object[] values) => sBuilder.AppendFormat("<p><strong>{0}: </strong>{1}</p>\n", property, String.Format(formatString, values));

            /// <summary>
            /// Method allows to add a html formatting that is read as a string
            /// </summary>
            /// <param name="val">variable that takes the string input and uses it as a HTML</param>
            public void AddHtml(string val) => sBuilder.AppendLine(val);

            /// <summary>
            /// Method that adds the html format with the format that allows a value to be inserted there
            /// </summary>
            /// <param name="format">takes a string with the format specifier</param>
            /// <param name="values">takes the value to put into the parameters (element of the array - database)</param>
            public void AddHtml(string format, params string[] values) => sBuilder.AppendFormat(format + "\n", values);

            /// <summary>
            /// Allows to control elements of the ASP.NET 
            /// </summary>
            /// <param name="val">takes any element that is part of the ASP.NET</param>
            public void AddControl(Control val) => Controls.Add(val);

            /// <summary>
            /// Method that allows to pass a list of ASP.NET elements
            /// </summary>
            /// <param name="val">uses the array to pass multiple items</param>
            public void AddControl(params Control[] val)
            {
                ///each element of the array and adds the controls to that ASP.NET element
                foreach (var item in val)
                {
                    AddControl(item);
                }
            }

            /// <summary>
            /// Method that takes in HTML as a string and sets it to be the footer
            /// </summary>
            /// <param name="val">variable that takes in a string as an argument</param>
            public void SetFooterHtml(String val) => Footer = val;

            /// <summary>
            /// Method that creates a string using pre-defined StringBuilder
            /// </summary>
            /// <returns>returns the string</returns>
            public new string ToString() => sBuilder.ToString();

            /// <summary>
            /// Method that diposes object
            /// </summary>
            public void Dispose() => sBuilder.Clear();

            /// <summary>
            /// Creates the controls as an object
            /// </summary>
            /// <param name="collapseAll">boolean true of false to determine weather to close other dropdowns or not</param>
            /// <returns></returns>
            public ExpandableControl ToObject(bool collapseAll = true) =>
                Controls.Any() ? GetExpandableControl(Title, Page, ToString(), Controls, collapseAll) : GetExpandableControl(Title, Page, ToString(), Footer, collapseAll);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="page"></param>
        /// <param name="content_html"></param>
        /// <param name="controls"></param>
        /// <param name="collapseAll"></param>
        /// <returns></returns>
        public static ExpandableControl GetExpandableControl(string title, Page page, string content_html, List<Control> controls, bool collapseAll = true)
        {
            ExpandableControl control = page.LoadControl("~/Controls/ExpandableControl.ascx") as ExpandableControl;
            control.Title = title;

            ExpandableControl.InnerControl inner = new ExpandableControl.InnerControl();
            inner.Controls.Add(page.ParseControl(content_html));
            control.Contents = inner;

            ExpandableControl.InnerControl footer = new ExpandableControl.InnerControl();
            controls.ForEach(b => footer.Controls.Add(b));
            control.Buttons = footer;
            control.CollapseAllUponExpanding = collapseAll;

            return control;
        }

        public static ExpandableControl GetExpandableControl(string title, Page page, string content_html = "", string footer_html = "", bool collapseAll = true)
        {
            ExpandableControl control = page.LoadControl("~/Controls/ExpandableControl.ascx") as ExpandableControl;
            control.Title = title;

            if (!string.IsNullOrEmpty(content_html))
            {
                ExpandableControl.InnerControl inner = new ExpandableControl.InnerControl();
                inner.Controls.Add(page.ParseControl(content_html));
                control.Contents = inner;
            }

            if (!string.IsNullOrEmpty(footer_html))
            {
                ExpandableControl.InnerControl footer = new ExpandableControl.InnerControl();
                footer.Controls.Add(page.ParseControl(footer_html));
                control.Buttons = footer;
            }
            control.CollapseAllUponExpanding = collapseAll;

            return control;
        }

        public static ExpandableControl GetExpandableControl(string title, string link, Page page, bool collapseAll = true)
        {
            ExpandableControl control = page.LoadControl("~/Controls/ExpandableControl.ascx") as ExpandableControl;
            control.Title = title;
            control.IsExpandable = false;
            control.URL = link;
            control.CollapseAllUponExpanding = collapseAll;
            return control;
        }

        public static string GetDateSuffix(DateTime date) => (date.Day % 10 == 1 && date.Day != 11) ? "st" : (date.Day % 10 == 2 && date.Day != 12) ? "nd" : (date.Day % 10 == 3 && date.Day != 13) ? "rd" : "th";

        /// <summary>
        /// Method that creates the dropdown with all the infomation and in the format specified here
        /// </summary>
        /// <param name="item">variable that uses the item of the array in appointment</param>
        /// <param name="page">Specifies the page that will be used for this</param>
        /// <param name="eventobj">specifies in which event should occur</param>
        /// <returns></returns>
        public static Control CreateAppointmentControl(BusinessObject.Appointment item, BasePage page, Func<BusinessObject.Appointment, bool> eventobj)
        {
            ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("<strong>{0}, {1}</strong> - {2} - {3}", item.Patient.User.Surname, item.Patient.User.Forename, item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString()), page);
            builder.AddProperty("Patient", @"<a href=""{0}?id={1}"">{2}, {3}</a>", Helper.PageAddress(Helper.Pages.VIEW_PROFILE), item.PatientUserID.ToString(), item.Patient.User.Surname, item.Patient.User.Forename);
            builder.AddProperty("Date", "{0:dddd dd}{1} {0:MMMM yyyy}", item.Appointment_DateTime, GetDateSuffix(item.Appointment_DateTime));
            builder.AddProperty("Time", "{0} - {1}", item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString());
            builder.AddProperty("Room", item.Room);

            builder.AddHtml("<hr>");
            builder.AddProperty("Reference", item.Ref_Number);
            if (eventobj != null && page.HasPermission(BasePage.Permissions.MODIFY_APPOINTMENT_INFO))
            {
                Button btnRemove = new Button() { Text = "Remove appointment...", CssClass = "button" };
                btnRemove.Click += (s, e) => eventobj(item);
                builder.AddControl(btnRemove);
            }
            return builder.ToObject();
        }

        public static Control CreateAppointmentControl(BusinessObject.Appointment item, Page page, bool hideLinks)
        {
            ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("<strong>{0}, {1}</strong> - {2} - {3}", item.Patient.User.Surname, item.Patient.User.Forename, item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString()), page);
            if (!hideLinks) builder.AddProperty("Patient", @"<a href=""{0}?id={1}"">{2}, {3}</a>", Helper.PageAddress(Helper.Pages.VIEW_PROFILE), item.PatientUserID.ToString(), item.Patient.User.Surname, item.Patient.User.Forename);
            builder.AddProperty("Date", "{0:dddd dd}{1} {0:MMMM yyyy}", item.Appointment_DateTime, GetDateSuffix(item.Appointment_DateTime));
            builder.AddProperty("Time", "{0} - {1}", item.Appointment_DateTime.ToShortTimeString(), item.Appointment_DateTime.AddMinutes(item.Appointment_Duration_Minutes).ToShortTimeString());
            builder.AddProperty("Room", item.Room);
            builder.AddProperty("Doctor", "{0} {1}", item.Staff.User.Forename, item.Staff.User.Surname);

            builder.AddHtml("<hr>");
            builder.AddProperty("Reference", item.Ref_Number);
            return builder.ToObject();
        }
    }
}