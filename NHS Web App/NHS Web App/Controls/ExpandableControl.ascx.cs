using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Controls
{
    [ParseChildren(true)]
    public partial class ExpandableControl : UserControl
    {
        public string UID;

        public bool CollapseAllUponExpanding = true;
        public bool IsExpandable = true;
        public string URL = "/Default.aspx";

        public class InnerControl : Control, ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                container.Controls.Add(this);
            }
        }

        public class LiteralContainer : Control, INamingContainer { }

        [TemplateContainer(typeof(LiteralContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Contents { get; set; }

        [TemplateContainer(typeof(LiteralContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Buttons { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string Title { get; set; }

        protected void Page_Init()
        {
            UID = Guid.NewGuid().ToString();
            if (IsExpandable)
            {
                if (Contents != null)
                {
                    LiteralContainer container = new LiteralContainer();
                    Contents.InstantiateIn(container);
                    placeholder.Controls.Add(container);
                }

                if (Buttons != null)
                {
                    LiteralContainer container = new LiteralContainer();
                    Buttons.InstantiateIn(container);
                    buttonHolder.Controls.Add(container);
                }
            }
        }
    }
}