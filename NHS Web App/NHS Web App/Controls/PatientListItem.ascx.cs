using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Controls
{
    public partial class PatientListItem : System.Web.UI.UserControl
    {
        public int patientid { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}