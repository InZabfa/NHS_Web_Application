using DataLayer;
using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Medicines : BasePage
    {
        public Medicines() : base(Permissions.VIEW_MEDICINES) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddMedicines();
            AddConditions();
            AddProviders();
        }

        protected void AddConditions()
        {
            conditions_list.Controls.Clear();
            foreach (var item in DB.ConditionsGet())
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(item.Name, this);
                builder.AddProperty("Date Added", item.Date_Added.ToShortDateString());
                builder.AddProperty("Added by staff", "{0}, {1}", item.Staff.User.Surname, item.Staff.User.Forename);
                if (item.Additional_Info.Length > 0) { builder.AddHtml("<hr />"); builder.AddProperty("Additional info", item.Additional_Info); }

                if (HasPermission(Permissions.MODIFY_MEDICINE))
                {
                    Button btnRemove = new Button() { Text = "Remove", CssClass = "button" };
                    //Performs the param method and then performs save changes.
                    btnRemove.Click += (s, e) => DB.SaveChanges(new Action<BusinessObject.Condition>(DB.ConditionRemove), item);
                    builder.AddControl(btnRemove);
                }

                conditions_list.Controls.Add(builder.ToObject());
            }

            if (!(conditions_list.Controls.Count > 0)) conditions_list.Controls.Add(new HtmlGenericControl("p") { InnerText = conditions_list.Attributes["data-empty-text"].ToString() });
        }

        protected void AddMedicines()
        {
            medicines_list.Controls.Clear();
            foreach (var item in DB.MedicationsGet())
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(item.Name, this);
                builder.AddProperty("Provider", item.Provider_Info.Name);
                builder.AddProperty("Max Dosage Per Day", item.Max_Dosage_Per_Day.ToString());
                builder.AddProperty("Max Dosage Per Week", item.Max_Dosage_Per_Week.ToString());
                builder.AddProperty("Quantity", item.Stock.Quantity.ToString());

                if (HasPermission(Permissions.MODIFY_MEDICINE))
                {
                    Button btnRemove = new Button() { Text = "Remove", CssClass = "button" };
                    //Performs the param method and then performs save changes.
                    btnRemove.Click += (s, e) => DB.SaveChanges(new Action<BusinessObject.Medication>(DB.MedicationRemove), item);
                    builder.AddControl(btnRemove);
                }

                medicines_list.Controls.Add(builder.ToObject());
            }

            if (!(medicines_list.Controls.Count > 0)) medicines_list.Controls.Add(new HtmlGenericControl("p") { InnerText = medicines_list.Attributes["data-empty-text"].ToString() });
        }

        protected void AddProviders()
        {
            providers_list.Controls.Clear();
            foreach (var item in DB.ProvidersGet())
            {
                ControlHandler.Builder builder = new ControlHandler.Builder(item.Name, this);
                builder.AddProperty("Email", "<a href=\"mailto:{0}\">{0}</a>", item.Email_Address);
                builder.AddProperty("Phone", "<a href=\"tel:{0}\">{0}</a>", item.Phone_Number.ToString());
                builder.AddProperty("Address", item.Address);

                if (HasPermission(Permissions.MODIFY_MEDICINE))
                {
                    Button btnRemove = new Button() { Text = "Remove", CssClass = "button" };
                    //Performs the param method and then performs save changes.
                    btnRemove.Click += (s, e) => DB.SaveChanges(new Action<BusinessObject.Provider_Info>(DB.ProviderRemove), item);
                    builder.AddControl(btnRemove);
                }

                providers_list.Controls.Add(builder.ToObject());
            }

            if (!(providers_list.Controls.Count > 0)) providers_list.Controls.Add(new HtmlGenericControl("p") { InnerText = providers_list.Attributes["data-empty-text"].ToString() });
        }
    }
}