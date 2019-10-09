using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Helper
    {
        public enum AccessType
        {
            NO_ACCESS,
            LESS_ACCESS,
            NORMAL_ACCESS,
            PRIVILEGED_ACCESS,
            ENHANCED_ACCESS,
            COMPLETE_ACCESS
        }

        /// <summary>
        /// Function to parse integer and return enum type AccessType
        /// </summary>
        /// <param name="acc">Access Type as an integer</param>
        /// <returns>AccessType from integer</returns>
        public static AccessType Parse(int acc)
        {
            return ((AccessType)acc);
        }

        /*  
         *  NO_ACCESS = USER HAS NO ACCESS TO MODIFY DATABASE, ONLY VIEW APPROPRIATE PAGES
         *  LESS_ACCESS = USER CAN ONLY MODIFY THEIR OWN DETAILS AND VIEW THEIR OWN APPOINTMENTS. 
         *  NORMAL_ACCESS = APPOINTMENTS CAN BE CREATED (BY STAFF) AND MEDICAL INFORMATION FOR PATIENTS CAN BE MODIFIED.
         *  PRIVILEDGED_ACCESS = MEDICAL DETAILS CAN BE MODIFIED 
         *  ENHANCED_ACCESS = STOCK CAN BE ADJUSTED
         */

        public enum Pages
        {
            OVERVIEW,
            CONTACT,
            MEDICINES,
            PATIENTS,
            STAFF,
            WELCOME,
            REPORTS,
            ACCOUNT,
            APPOINTMENTS,
            TEST,
            LOGIN,
            LOGOUT,
            QUEUE,
            VIEW_MEDICINE,
            VIEW_PROFILE,
            DISABLED_ACCOUNT,
            CREATE_PAITENT,
            CREATE_PAITENT_CONDITION,
            CREATE_MEDICINE,
            CREATE_APPOINTMENT,
            CREATE_CONDITION,
            CREATE_PROVIDER_INFO,
            CREATE_NOTE,
            CREATE_PRACTICE,
            CREATE_STAFF,
            CREATE_USER,
            PATIENT_LANDING,
            FIND_US,
            CHANGE_PASSWORD
        }

        public static string PageAddress(Pages pageType)
        {
            switch (pageType)
            {
                case Pages.OVERVIEW:
                    return @"\Default.aspx";
                case Pages.PATIENT_LANDING:
                    return @"\Patient\Default.aspx";
                case Pages.FIND_US:
                    return @"\Patient\FindUs.aspx";
                case Pages.CONTACT:
                    return @"\Pages\Contact.aspx";
                case Pages.CREATE_PAITENT:
                    return @"\Pages\Create\Patient.aspx";
                case Pages.CREATE_PAITENT_CONDITION:
                    return @"\Pages\Create\PatientCondition.aspx";
                case Pages.MEDICINES:
                    return @"\Pages\Medicines.aspx";
                case Pages.PATIENTS:
                    return @"\Pages\Patients.aspx";
                case Pages.STAFF:
                    return @"\Pages\Staff.aspx";
                case Pages.WELCOME:
                    return @"\Pages\Welcome.aspx";
                case Pages.REPORTS:
                    return @"\Pages\Reports.aspx";
                case Pages.QUEUE:
                    return @"\Queue";
                case Pages.APPOINTMENTS:
                    return @"\Pages\Appointments.aspx";
                case Pages.ACCOUNT:
                    return @"\Pages\Account.aspx";
                case Pages.TEST:
                    return @"\Test.aspx";
                case Pages.CREATE_NOTE:
                    return @"\Pages\Create\Note.aspx";
                case Pages.CREATE_PROVIDER_INFO:
                    return @"\Pages\Create\ProviderInfo.aspx";
                case Pages.CREATE_APPOINTMENT:
                    return @"\Pages\Create\Appointment.aspx";
                case Pages.CREATE_MEDICINE:
                    return @"\Pages\Create\Medicine.aspx";
                case Pages.CREATE_USER:
                    return @"\Pages\Create\User.aspx";
                case Pages.LOGIN:
                    return @"\Login.aspx";
                case Pages.CHANGE_PASSWORD:
                    return @"\ChangePassword.aspx";
                case Pages.LOGOUT:
                    return @"\Logout.aspx";
                case Pages.VIEW_MEDICINE:
                    return @"\Pages\View\Medicine.aspx";
                case Pages.CREATE_PRACTICE:
                    return @"\Pages\Create\Practice.aspx";
                case Pages.VIEW_PROFILE:
                    return @"\Pages\View\Profile.aspx";
                case Pages.DISABLED_ACCOUNT:
                    return @"\Pages\Disabled.aspx";
                case Pages.CREATE_CONDITION:
                    return @"\Pages\Create\Condition.aspx";
                case Pages.CREATE_STAFF:
                    return @"\Pages\Create\Staff.aspx";
                default:
                    return @"\Default.aspx";
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] ConvertToBinary(string str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public static String ConvertFromBytes(byte[] src)
        {
            return new ASCIIEncoding().GetString(src);
        }


        public static string GetHTMLString(string dir, params string[] vals)
        {
            return String.Format(System.IO.File.ReadAllText(dir), vals);
        }

        public static User Login(String email, String password, Repository re)
        {
            User user = re.UserGet(email);
            if (user != null)
                if (Encryption.VerifySha256(user.Password, password))
                    return user;
            return null;
        }

        public static bool Email(string from, string email, string subj, string msg_or_html)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress(from);
                mail.Subject = subj;
                mail.Body = msg_or_html;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
            }
            catch (Exception ex) { throw ex; }
            return true;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder builder = new StringBuilder();
            Random rand = new Random();

            while (0 < length--)
            {
                builder.Append(valid[rand.Next(valid.Length)]);
            }
            return builder.ToString();
        }
    }
}
