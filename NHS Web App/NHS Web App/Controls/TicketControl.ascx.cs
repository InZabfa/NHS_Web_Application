using System;
using System.Web.UI;

namespace NHS_Web_App.Controls
{
    public partial class TicketControl : UserControl
    {
        public enum Type
        {
            seen,
            late,
            ready,
            @default
        }

        public Type TicketType { get; set; } = Type.@default;
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Footer { get; set; } = "";
        public string Link { get; set; }

        public string GetClass()
        {
            return "ticket " + Enum.GetName(typeof(TicketControl.Type), TicketType).ToLower();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataBindChildren();
        }
    }
}