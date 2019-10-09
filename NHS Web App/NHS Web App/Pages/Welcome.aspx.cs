using BusinessObject;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHS_Web_App.Pages
{
    public partial class Welcome : BasePage
    {
        public Welcome() : base(false) { }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DB.HasAdmin())
            {
                Response.Redirect(Helper.PageAddress(Helper.Pages.OVERVIEW));
            }

            DataBindChildren();
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkConfirmed())
                {
                    bool dtCorrect = Validator.IsDateTimeCorrect(txtDOB.Text) != null, eCorrect = Validator.IsEmailValid(txtEmail.Text) && Validator.IsEmailValid(txtPracticeEmail.Text), prac_e_Correct = Validator.IsEmailValid(txtPracticeEmail.Text)
                        , hasUserEmailAlready = DB.UserGet(txtEmail.Text) != null;

                    if (dtCorrect && eCorrect && prac_e_Correct)
                    {
                        if (hasUserEmailAlready)
                        {
                            ShowMessage("Oops!", "a user with this email address already exists...", true, MessageType.ERROR);
                        }
                        else
                        {
                            User admin = new User() { Forename = txtFName.Text, Email = txtEmail.Text, Password = Encryption.GetSha256(txtPassword.Text), Surname = txtLName.Text, DOB = DateTime.Parse(txtDOB.Text), Gender = int.Parse(pckGender.SelectedValue) == 1, Address = txtPersonalAddress.Text, Phone_number = txtPersonalPhoneNum.Text };
                            Practice_Info pInfo = new Practice_Info() { Address = txtPracticeAddress.Text, Email = txtPracticeEmail.Text, Name = txtPracticeName.Text, Phone_Number = txtContactNumber.Text };
                            admin.Practice_Info = pInfo;
                            admin.Staffs.Add(new BusinessObject.Staff() { User = admin, Staff_Role = "admin", FT_PT = true, Contract_type = "permanent", Working_hours = 40, Working_days = "1111100" });
                            admin.Access_Levels = new Access_Levels() { Access_Level = 6, Access_Enabled = true };
                            DB.UserAdd(admin);
                            DB.SaveChanges();
                            Response.Redirect(Helper.PageAddress(Helper.Pages.LOGIN));
                        }
                    }
                    else
                    {
                        if (!dtCorrect)
                            ShowMessage("Oops!", "your date of birth doesn't seem to be correct, fix this and try again...", false, MessageType.ERROR);

                        if (!eCorrect)
                            ShowMessage("Oops!", "your email address doesn't seem to be correct, fix this and try again...", false, MessageType.ERROR);
                    }
                }
                else
                {
                    ShowMessage("Oops!", "confirm you agree to any changes and information entered and try again...", false, MessageType.ERROR);
                }
            }
            catch (Exception)
            {
                ShowMessage("Oops!", "an error occured. Please refresh the page and try again...", false, MessageType.ERROR);
            }
        }

        protected void chkConfirmAccess_CheckedChanged(object sender, EventArgs e)
        {
            checkConfirmed();
        }

        protected void chkConfirmDetails_CheckedChanged(object sender, EventArgs e)
        {
            checkConfirmed();
        }

        protected bool checkConfirmed()
        {
            return chkConfirmAccess.Checked && chkConfirmDetails.Checked;
        }
    }
}