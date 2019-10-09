using System;

namespace NHS_Web_App.Queue
{
    public partial class Default : BasePage
    {
        public Default() : base(MedicalPermissions()) { }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}