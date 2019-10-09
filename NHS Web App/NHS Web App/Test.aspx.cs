using BusinessObject;
using DataLayer;
using System;

namespace NHS_Web_App
{
    public partial class Test : BasePage
    {
        public Test() : base(MedicalPermissions()) { }

        protected void AddAccessLevels()
        {
            DB.AccessTypesClear();
            DB.SaveChanges();

            foreach (ROLES item in Enum.GetValues(typeof(ROLES)))
            {
                DB.AccessTypesAdd(new Access_Types() { AccessLevel = (int)item, Name = item.ToString() });
            }

            DB.SaveChanges();
            Response.Write("Successful! Added access types.");
        }

        protected void btnCreateTestData_Click(object sender, EventArgs e)
        {
            Practice_Info p_info = new Practice_Info();
            p_info.Address = address.Text;
            p_info.Email = email.Text;
            p_info.Name = pName.Text;
            p_info.Phone_Number = phonenum.Text;

            DB.PracticeAdd(p_info);

            Emergency_Contacts eContact = new Emergency_Contacts();
            eContact.Address = address.Text;
            eContact.Forename = fname.Text;
            eContact.Phone_Number = phonenum.Text;
            eContact.Relation = "Family Friend";
            eContact.Surname = sname.Text;
            DB.EmergencyContactAdd(eContact);

            User user = new User();
            Access_Levels access = new Access_Levels();
            user.Access_Levels = access;

            user.Email = email.Text;
            user.Password = Encryption.GetSha256(pwrd.Text);
            user.Forename = fname.Text;
            user.Surname = sname.Text;
            user.Address = address.Text;

            user.Emergency_Contacts.Add(eContact);
            user.DOB = DateTime.Parse(dob.Text);
            user.Phone_number = phonenum.Text;
            user.Gender = chkMale.Checked;
            user.Practice_Info = p_info;

            DB.UserAdd(user);
            DB.SaveChanges();
            Response.Write("Successful...");
        }


        public void sampleData()
        {
            Practice_Info p_info = new Practice_Info();
            p_info.Address = "123 Brownlow Hill, Brownlow, Rosa, AB18 34D";
            p_info.Email = "brownlowhill@nhs.co.uk";
            p_info.Name = "Brownlow Hill";
            p_info.Phone_Number = "01283112287";
            DB.PracticeAdd(p_info);

            Emergency_Contacts eContact = new Emergency_Contacts();
            eContact.Address = "123 Example Street, Random Place, Rosa, AB12 34D";
            eContact.Forename = "Sarah";
            eContact.Phone_Number = "+44753716939";
            eContact.Relation = "Family Friend";
            eContact.Surname = "Clarke";
            DB.EmergencyContactAdd(eContact);

            User user = new User();
            Access_Levels access = new Access_Levels();
            access.Access_Level = 6;
            user.Access_Levels = access;

            user.Email = "test@example.com";
            user.Password = Encryption.GetSha256("password1234");
            user.Forename = "Robert";
            user.Surname = "Michaels";
            user.Address = "84 Long Road, Barton, Stenzenhill, L12 0WZ";

            user.Emergency_Contacts.Add(eContact);
            user.DOB = DateTime.Parse("12/04/1973");
            user.Phone_number = "01234567890";
            user.Gender = true;
            user.Practice_Info = p_info;

            DB.UserAdd(user);
            DB.SaveChanges();
            Response.Write("Successful...\n");
            Response.Write("Login for test account is: " + user.Email + ", password: password1234 (MD5 hash: " + user.Password + ")...");
        }

        protected void btnAuto_Click(object sender, EventArgs e)
        {
            sampleData();
        }

        protected void btnTestEmail_Click(object sender, EventArgs e)
        {
            string body = "<p><strong>Hello!</strong></p><br><p>This is a test email...</p>";
            Helper.Email(LoggedInUser.Email, LoggedInUser.Email, "Testing", body);
            Response.Write(Helper.Email(LoggedInUser.Practice_Info.Email, email.Text, "Welcome! Your Login deltais", body));
        }

        protected void btnCreateAccessTypes_Click(object sender, EventArgs e) => AddAccessLevels();
    }
}