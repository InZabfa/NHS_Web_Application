using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App
{
    public partial class ChangePassword : BasePage
    {
        public ChangePassword() : base(false) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoggedInUser == null) Redirect(DataLayer.Helper.Pages.LOGIN);
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            bool enteredPwrdsCorrect = txtConfirmPassword.Text == txtNewPassword.Text;

            if (DataLayer.Encryption.GetSha256(txtCurrentPassword.Text) != LoggedInUser.Password)
            {
                ShowMessage("Oops!", "your account password is incorrect, try again...", false, MessageType.ERROR);
                return;
            }

            if (!enteredPwrdsCorrect)
            {
                ShowMessage("Oops!", "the entered passwords do not match, try again...", false, MessageType.ERROR);
                return;
            }

            DB.UserGet(LoggedInUser.Id).Password = DataLayer.Encryption.GetSha256(txtConfirmPassword.Text);
            DB.SaveChanges();
            ShowMessage("Success!", "your password has updated!", false, MessageType.SUCCESS);
        }
    }
}