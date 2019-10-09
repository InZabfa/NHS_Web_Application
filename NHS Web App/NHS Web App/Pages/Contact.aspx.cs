using DataLayer;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class Contact : BasePage
    {
        public Contact() : base(Permissions.VIEW_CONTACTS) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownMenu();
            }
        }

        protected BusinessObject.Practice_Info GetSelectedPractice()
        {
            int val = Validator.IsIntegerCorrect(selected_practice.SelectedValue, -1);
            return val > -1 ? DB.PracticeGet(val) : null;
        }

        private void DropDownMenu()
        {
            foreach (var p in DB.PatientsGet(DB.StaffGet(LoggedInUser)))
            {
                selected_patient.Items.Add(new ListItem(string.Format("{0}, {1} ({2})", p.User.Forename, p.User.Surname, p.User.Email), p.UserID.ToString()));
            }

            foreach (var pr in DB.PracticeGetAll())
            {
                selected_practice.Items.Add(new ListItem(string.Format("{0} ({1})", pr.Name, pr.Email), pr.Id.ToString()));
            }

        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                int user_id = Validator.IsIntegerCorrect(selected_patient.SelectedValue.ToString(), -1);
                int practice_id = Validator.IsIntegerCorrect(selected_practice.SelectedValue.ToString(), -1);

                if (!(user_id > -1 || practice_id > -1))
                {
                    ShowMessage("Oops!", "couldn't transfer this patient...", false, MessageType.ERROR);
                    return;
                }

                BusinessObject.User user = DB.UserGet(user_id);
                user.PracticeID = practice_id;
                DB.Update(user);
                DB.SaveChanges();

                ShowMessage("Success", "transferred this patient to this practice...", true, MessageType.SUCCESS);
            }
            catch
            {
                ShowMessage("Oops!", "an error occured, refresh the page and try again...", false, MessageType.ERROR);
            }
        }
    }


}