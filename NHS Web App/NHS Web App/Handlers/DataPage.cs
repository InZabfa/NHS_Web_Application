using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace NHS_Web_App.Handlers
{
    public class DataPage
    {
        public List<DataPageSection> Sections = new List<DataPageSection>();

        public static void Render(DataPage dp, Control container)
        {
            foreach (DataPageSection s in dp.Sections)
            {
                HtmlGenericControl box = new HtmlGenericControl("div");
                box.Attributes.Add("class", "box");
                HtmlGenericControl boxHeader = new HtmlGenericControl("div");
                boxHeader.Attributes.Add("class", "box-header");
                HtmlGenericControl hTwo = new HtmlGenericControl("h2") { InnerText = s.Title };
                HtmlGenericControl pg = new HtmlGenericControl("p") { InnerText = s.Description };
                boxHeader.Controls.Add(hTwo);
                boxHeader.Controls.Add(pg);
                box.Controls.Add(boxHeader);
                foreach (DataSectionFieldGroup fG in s.Fields)
                {
                    if (fG.GetType() == typeof(DataSectionSplitFieldGroup))
                    {
                        DataSectionSplitFieldGroup element = fG as DataSectionSplitFieldGroup;
                        HtmlGenericControl f_fs = new HtmlGenericControl("fieldset");
                        f_fs.Attributes.Add("class", "half");

                        f_fs.Controls.Add(new HtmlGenericControl("label") { InnerText = element.First_Control.Caption });
                        f_fs.Controls.Add(element.First_Control.inputControl);
                        box.Controls.Add(f_fs);

                        HtmlGenericControl s_fs = new HtmlGenericControl("fieldset");
                        s_fs.Attributes.Add("class", "half");

                        s_fs.Controls.Add(new HtmlGenericControl("label") { InnerText = element.Second_Control.Caption });
                        s_fs.Controls.Add(element.Second_Control.inputControl);
                        box.Controls.Add(s_fs);
                    }
                    else
                    {
                        DataSectionFieldGroup element = fG;
                        HtmlGenericControl fs = new HtmlGenericControl("fieldset");
                        fs.Controls.Add(new HtmlGenericControl("label") { InnerText = element.Control.Caption });
                        fs.Controls.Add(element.Control.inputControl);
                        box.Controls.Add(fs);
                    }
                }
                if (container != null)
                    container.Controls.Add(box);
            }
        }
    }

    public class DataPageSection
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public List<DataSectionFieldGroup> Fields = new List<DataSectionFieldGroup>();

        public DataPageSection(String title, String descr)
        {
            Description = descr;
            Title = title;
        }
    }

    public class DataSectionFieldGroup
    {
        public DataSectionElement Control;
    }

    public class DataSectionSplitFieldGroup : DataSectionFieldGroup
    {
        public DataSectionElement First_Control { get; set; }
        public DataSectionElement Second_Control { get; set; }

        public DataSectionSplitFieldGroup(DataSectionElement fC, DataSectionElement sC)
        {
            First_Control = fC;
            Second_Control = sC;
        }
    }

    public class DataSectionElement
    {
        public String Caption { get; set; }
        public System.Web.UI.Control inputControl { get; set; }

        public DataSectionElement(string caption, System.Web.UI.Control ctrl)
        {
            inputControl = ctrl;
            Caption = caption;
        }
    }
}