using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Controls
{
    [ParseChildren(true)]
    public partial class DialogControl : System.Web.UI.UserControl
    {
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
        public ITemplate Body { get; set; }

        [TemplateContainer(typeof(LiteralContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Header { get; set; }


        protected void Page_Init()
        {
            if (Body != null)
            {
                LiteralContainer container = new LiteralContainer();
                Body.InstantiateIn(container);
                body.Controls.Add(container);
            }

            if (Header != null)
            {
                LiteralContainer container = new LiteralContainer();
                Header.InstantiateIn(container);
                header.Controls.Add(container);
            }
        }
    }
}