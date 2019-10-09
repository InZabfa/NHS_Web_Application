using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Controls
{
    public partial class MessageControl : UserControl
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public bool ShowClose { get; set; }
        public String Name { get; set; }
        public BasePage.MessageType msgType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        protected override void OnLoad(EventArgs e)
        {
            Name = Guid.NewGuid().ToString();

            switch (msgType)
            {
                case BasePage.MessageType.WARNING:
                    msgcontrol.Style.Add("background-color", "#deb41b");
                    break;
                case BasePage.MessageType.INFORM:
                    msgcontrol.Style.Add("background-color", "#005eb8");
                    break;
                case BasePage.MessageType.ERROR:
                    msgcontrol.Style.Add("background-color", "#de1b48");
                    break;
                case BasePage.MessageType.SUCCESS:
                    msgcontrol.Style.Add("background-color", "#519f43");
                    break;
                default:
                    msgcontrol.Style.Add("background-color", "#3f4e60");
                    break;
            }
            base.OnLoad(e);
        }
    }
}