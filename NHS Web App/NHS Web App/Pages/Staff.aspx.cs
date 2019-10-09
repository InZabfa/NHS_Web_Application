using DataLayer;
using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Staff : BasePage
    {
        public Staff() : base(Permissions.VIEW_STAFF) { }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadColleagues();
            }
        }

        protected void LoadColleagues()
        {
            List<BusinessObject.Staff> staff = DB.StaffGetAll();
            foreach (var item in staff)
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(string.Format("{0}, {1}", item.User.Surname, item.User.Forename), this);
                builder.AddProperty("Role", Validator.FirstLetterToUpper(item.Staff_Role));
                builder.AddProperty("Part-time/Full-time", item.FT_PT ? "Full-time" : "Part-time");

                long days = long.Parse(item.Working_days);

                builder.AddProperty("Working days", "{0}", Validator.GetDictionaryString(Validator.GetDays(days)));
                builder.AddProperty("Working hours", item.Working_hours.ToString());
                colleague_list.Controls.Add(builder.ToObject());
            }
        }
    }
}