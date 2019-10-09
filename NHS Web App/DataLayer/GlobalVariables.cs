using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GlobalVariables
    {
        #region Password
        /// <summary>
        /// Password minimum length of chars
        /// </summary>
        public const int PASSWORD_MIN_LENGTH = 8;
        /// <summary>
        /// Password maximum number of chars
        /// </summary>
        public const int PASSWORD_MAX_LENGTH = 256;
        #endregion

        #region Working Hours

        /// <summary>
        /// The hour of 24-hours when work begins
        /// </summary>
        public const int START_WORK_HOUR = 9;

        /// <summary>
        /// The hour of 24-hours when work ends
        /// </summary>
        public const int END_WORK_HOUR = 20;
        #endregion

        #region Cookies
        /// <summary>
        /// Cookie timeout in minutes
        /// </summary>
        public const int COOKIE_TIMEOUT = 60;
        #endregion

        #region Access Levels
        public enum ACCESS_LEVELS
        {
            DISABLED = 0,
            HOME_USER = 1,
            DISABLED_STAFF = 2,
            RECEPTIONIST = 3,
            MEDICAL_STAFF = 4,
            DOCTOR = 5,
            ADMIN = 6
        }
        #endregion

        #region Messages
        public const String MESSAGE_PAGENOTFOUND = "Could not find the page you were looking for...";
        public const String MESSAGE_NOTAUTHORISED = "You do not have the correct access level to access this page...";
        public const String ERROR_PAGETITLE = "Oops!";
        #endregion
    }
}
