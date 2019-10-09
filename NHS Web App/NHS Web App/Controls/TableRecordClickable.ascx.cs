using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class TableRecordClickable : System.Web.UI.UserControl
    {
        public int UserID;
        public String Name;
        public int Age;
        public DateTime AppointmentTime;

        public TableRecordClickable(int uid, String name, int age, String datetime)
        {
            UserID = uid;
            Name = name;
            Age = age;
            AppointmentTime = DateTime.Parse(datetime);
        }

        public TableRecordClickable()
        {

        }


        public void OnClick()
        {
            this.Visible = false;
        }
    }
}