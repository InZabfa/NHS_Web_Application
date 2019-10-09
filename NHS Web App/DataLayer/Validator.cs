using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Validator
    {
        /// <summary>
        /// Method to check if an email address is valid
        /// </summary>
        /// <param name="email">Email to validate</param>
        /// <returns>True or false if the email address is valid</returns>
        public static bool IsEmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if string is an integer, if it is, returns the string as an integer. Else returns -1.
        /// </summary>
        /// <param name="val">string to validate</param>
        /// <returns>parse integer if successful, else returns -1</returns>
        public static int IsInteger(string val)
        {
            try { return int.Parse(val); }
            catch { return -1; }
        }

        public static DateTime IsDateTimeCorrect(string dt, DateTime? defaultval = null)
        {
            try { return DateTime.Parse(dt); }
            catch { return defaultval == null ? DateTime.Now : defaultval.Value; }
        }

        /// <summary>
        /// Accepts the string to check if is a number. If it is and the integer value can be returned it is,
        /// else the defaultval is returned.
        /// </summary>
        /// <param name="val">value to parse</param>
        /// <param name="defaultval">upon failure, this value is returned</param>
        /// <returns></returns>
        public static int IsIntegerCorrect(string val, int defaultval = 15)
        {
            if (string.IsNullOrWhiteSpace(val))
                return defaultval;
            try { return int.Parse(val); }
            catch { return defaultval; }
        }


        /// <summary>
        /// Returns the entered string with the first letter as a capital 
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>string with first letter capitalised</returns>
        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        /// <summary>
        /// Method to check if a password is valid by ensuring it reaches minimum requirements.
        /// Checks if password has uppercase, lowercase and a digit.
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <returns>True/False if password is valid</returns>
        public static bool ValidatePassword(string password)
        {
            if (password == null) throw new ArgumentNullException();

            bool hasUpperCaseLetter = false, hasLowerCaseLetter = false, hasDecimalDigit = false, meetsLengthRequirements = password.Length >= GlobalVariables.PASSWORD_MIN_LENGTH && password.Length <= GlobalVariables.PASSWORD_MAX_LENGTH;
            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }
            return meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit;
        }

        public static string GetDictionaryString(Dictionary<string, bool> dict)
        {
            StringBuilder builder = new StringBuilder();
            dict.Where(i => i.Value == true).Select(i => i.Key).ToList().ForEach(x => builder.Append(" " + x).Append(","));
            return builder.ToString().Trim().TrimEnd(',');
        }

        public static Dictionary<string, bool> GetDays(long days)
        {
            char[] val = days.ToString().ToCharArray();
            Dictionary<string, bool> Days = new Dictionary<string, bool>();
            Days.Add("Monday", val[0] == char.Parse("1"));
            Days.Add("Tuesday", val[1] == char.Parse("1"));
            Days.Add("Wednesday", val[2] == char.Parse("1"));
            Days.Add("Thursday", val[3] == char.Parse("1"));
            Days.Add("Friday", val[4] == char.Parse("1"));
            Days.Add("Saturday", val[5] == char.Parse("1"));
            Days.Add("Sunday", val[6] == char.Parse("1"));
            return Days;
        }
    }
}
