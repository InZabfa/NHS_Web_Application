using NHS_Web_App.Controls;
using NHS_Web_App.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace NHS_Web_App
{
    public class BasePage : Page
    {
        private List<Permissions> _pagepermissions;
        protected List<Permissions> PagePermissions
        {
            get
            {
                return _pagepermissions;
            }
        }

        public bool EnableAuthorisation;

        public BasePage(bool enableauth = true)
        {
            EnableAuthorisation = enableauth;
        }

        public BasePage() : base()
        {
            _pagepermissions = CreatePermissions(Permissions.VIEW_BOOKING);
            EnableAuthorisation = true;
        }

        public BasePage(params Permissions[] permissions) : this(true)
        {
            _pagepermissions = new List<Permissions>(permissions);
        }

        public void ClearControls(params Control[] controls)
        {
            foreach (var item in controls)
            {
                item.ViewStateMode = ViewStateMode.Disabled;
                item.Controls.Clear();
                item.ViewStateMode = ViewStateMode.Enabled;
            }
        }

        /// <summary>
        /// Determines whether the URL contains edit query, if so and the value is an integer > -1 then return true, else false.
        /// </summary>
        public bool IsEditing
        {
            get
            {
                return EditingID > -1;
            }
        }

        public int EditingID
        {
            get
            {
                if (Request.QueryString["edit"] != null)
                    try
                    {
                        int val = int.Parse(Request.QueryString["edit"].ToString());
                        return val;
                    }
                    catch { return -1; }
                return -1;
            }
        }

        protected void Page_PreInit()
        {
            if (EnableAuthorisation)
                Handler.HandleAuthorisation(DB, this, LoggedInUser, Response, PagePermissions, Request.Url.PathAndQuery);
        }

        public delegate void PassSearch(String search_term);
        public event PassSearch OnPassSearch;

        public void Search(String val)
        {
            OnPassSearch(val);
        }

        /// <summary>
        /// Handles any return using query strings
        /// </summary>
        protected void HandleReturn()
        {
            if (Request.QueryString["return_url"] != null)
            {
                string returnURL = Request.QueryString["return_url"].ToString();
                Response.Redirect(returnURL);
            }
        }

        public enum ROLES
        {
            UNASSIGNED = 0,
            PATIENT = 1,
            NURSE = 2,
            RECEPTIONIST = 3,
            PHARMACIST = 4,
            DOCTOR = 5,
            ADMIN = 6
        }

        /// <summary>
        /// Returns the Enum ROLES type from Access Level integer
        /// </summary>
        /// <returns>ROLE type</returns>
        public ROLES GetRole()
        {
            return (ROLES)Enum.Parse(typeof(ROLES), LoggedInUser.Access_Levels.Access_Level.ToString());
        }

        public enum Permissions
        {
            MODIFY_MEDICINE,
            VIEW_OVERVIEW,
            VIEW_APPOINTMENTS,
            VIEW_PATIENTS,
            VIEW_REPORTS,
            VIEW_CONTACTS,
            VIEW_MEDICINES,
            DELETE_PATIENT,
            OPEN_QUEUE,
            MODIFY_GP_INFO,
            VIEW_STAFF,
            DEACTIVATE_STAFF,
            MODIFY_PATIENT_PERSONAL_INFO,
            ADD_STAFF,
            MODIFY_STAFF_INFO,
            CREATE_APPOINTMENT,
            VIEW_BOOKING,
            MODIFY_PATIENT_MEDICAL_INFO,
            MODIFY_APPOINTMENT_INFO,
            CREATE_PATIENT,
            TRANSFER_PATIENT,
            CREATE_PROVIDER_INFO,
            MODIFY_CONDITION
        }

        /// <summary>
        /// Creates the array of permissions for medical staff
        /// </summary>
        /// <returns>the array of permissions</returns>
        public static Permissions[] MedicalPermissions() => new Permissions[]
            {
                Permissions.VIEW_OVERVIEW,
                Permissions.VIEW_APPOINTMENTS,
                Permissions.VIEW_PATIENTS,
                Permissions.VIEW_MEDICINES,
                Permissions.VIEW_REPORTS,
                Permissions.VIEW_CONTACTS,
                Permissions.VIEW_BOOKING
            };

        /// <summary>
        /// Checks if the user has the permission provided
        /// </summary>
        /// <param name="perm">Permission to check</param>
        /// <returns>true if have permission, else false</returns>
        public bool HasPermission(Permissions perm) => GetPermission().Contains(perm);

        /// <summary>
        /// Checks that the default "VIEW_BOOKING" permission is provided
        /// </summary>
        /// <returns>True if they have VIEW_BOOKING</returns>
        public bool HasPermission() => GetPermission().Contains(Permissions.VIEW_BOOKING);

        /// <summary>
        /// Returns array containing user permissions for logged in user
        /// </summary>
        /// <returns>an array of permissions</returns>
        public List<Permissions> GetPermission()
        {
            bool isStaff = DB.UserIsStaff(LoggedInUser);

            if (LoggedInUser.Access_Levels == null) return CreatePermissions();

            int Access_Level = LoggedInUser.Access_Levels.Access_Level;

            // Nurse Access (Equiv to medical staff access)
            if (isStaff && Access_Level == 2) return CreatePermissions(MedicalPermissions());

            // Receptionist Access
            if (isStaff && Access_Level == 3) return CreatePermissions(MedicalPermissions(), Permissions.MODIFY_PATIENT_PERSONAL_INFO,
                                                                        Permissions.DELETE_PATIENT, Permissions.MODIFY_GP_INFO,
                                                                        Permissions.CREATE_APPOINTMENT, Permissions.MODIFY_APPOINTMENT_INFO,
                                                                        Permissions.OPEN_QUEUE, Permissions.CREATE_PATIENT, Permissions.TRANSFER_PATIENT);

            // Pharmacist
            if (isStaff && Access_Level == 4) return CreatePermissions(MedicalPermissions(), Permissions.MODIFY_MEDICINE, Permissions.CREATE_PROVIDER_INFO);

            // Doctor Access 
            if (isStaff && Access_Level == 5) return CreatePermissions(MedicalPermissions(), Permissions.MODIFY_PATIENT_MEDICAL_INFO, Permissions.MODIFY_CONDITION);

            // Admin Access

            //if (isStaff && Access_Level == 6) return CreatePermissions(MedicalPermissions(), Permissions.MODIFY_PATIENT_PERSONAL_INFO,
            //                                                            Permissions.DELETE_PATIENT, Permissions.MODIFY_GP_INFO,
            //                                                            Permissions.CREATE_APPOINTMENT, Permissions.MODIFY_APPOINTMENT_INFO,
            //                                                            Permissions.OPEN_QUEUE, Permissions.MODIFY_MEDICINE,
            //                                                            Permissions.MODIFY_PATIENT_MEDICAL_INFO, Permissions.VIEW_STAFF,
            //                                                            Permissions.DEACTIVATE_STAFF, Permissions.MODIFY_STAFF_INFO,
            //                                                            Permissions.ADD_STAFF, Permissions.CREATE_PATIENT, Permissions.TRANSFER_PATIENT);

            if (isStaff && Access_Level == 6) return Enum.GetValues(typeof(Permissions)).Cast<Permissions>().ToList();

            // Returns patient permission by default
            return CreatePermissions(Permissions.VIEW_BOOKING);
        }

        /// <summary>
        /// Creates a list of the permissions from arguments provided
        /// </summary>
        /// <param name="perms">An array of object as an inline argument</param>
        /// <returns>List of permissions create from array of objects</returns>
        private List<Permissions> CreatePermissions(params object[] perms)
        {
            List<Permissions> Perms = new List<Permissions>();
            foreach (var item in perms)
            {
                if (item.GetType() == typeof(Permissions))
                {
                    Perms.Add((Permissions)item);
                }

                if (item.GetType() == typeof(Permissions[]))
                {
                    Perms.AddRange((Permissions[])item);
                }
            }
            return Perms;
        }

        /// <summary>
        /// Redirect to a page with current page as return address.
        /// </summary>
        /// <param name="pageAddress"></param>
        /// <param name="pageTo"></param>
        protected void Redirect(DataLayer.Helper.Pages destination)
        {
            Response.Redirect(string.Format("{0}?return_url={1}", DataLayer.Helper.PageAddress(destination), Request.Url.PathAndQuery));
        }

        protected void Redirect(string destination)
        {
            Response.Redirect(string.Format("{0}?return_url={1}", destination, Request.Url.PathAndQuery));
        }

        protected void Redirect(DataLayer.Helper.Pages destination, params KeyValuePair<string, string>?[] arguments)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}", DataLayer.Helper.PageAddress(destination));
            builder.AppendFormat("?return_url={0}", Request.Url.LocalPath);
            foreach (var item in arguments)
            {
                if (item.HasValue)
                    builder.AppendFormat("&{0}={1}", item.Value.Key, item.Value.Value);
            }

            Response.Redirect(builder.ToString());
        }

        protected void Redirect(string destination, params KeyValuePair<string, string>?[] arguments)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}", destination);
            builder.AppendFormat("?return_url={0}", Request.Url.LocalPath);
            foreach (var item in arguments)
            {
                if (item.HasValue)
                    builder.AppendFormat("&{0}={1}", item.Value.Key, item.Value.Value);
            }

            Response.Redirect(builder.ToString());
        }

        public enum MessageType
        {
            WARNING,
            INFORM,
            ERROR,
            SUCCESS,
            NONE
        }

        public void CloseMessages()
        {
            Control container = Master.FindControl("default_msg_container");
            if (container != null)
            {
                container.Controls.Clear();
            }
        }

        public void ShowMessage(Control container, string title, string message, bool canClose, MessageType msgType)
        {
            if (container != null)
            {
                MessageControl msg = (MessageControl)Page.LoadControl("~/Controls/MessageControl.ascx");
                msg.Title = title;
                msg.Description = message;
                msg.ShowClose = canClose;
                msg.msgType = msgType;
                container.Controls.Add(msg);
            }
        }

        public void ShowMessage(string title, string message, bool canClose, MessageType msgType)
        {
            Control container = Master.FindControl("default_msg_container");
            if (container != null)
            {
                MessageControl msg = (MessageControl)Page.LoadControl("~/Controls/MessageControl.ascx");
                msg.Title = title;
                msg.Description = message;
                msg.ShowClose = canClose;
                msg.msgType = msgType;
                container.Controls.Add(msg);
            }
        }

        public bool IsSearchEventHandlerAdded()
        {
            return OnPassSearch != null && OnPassSearch.GetInvocationList().Length > 0;
        }


        public DataLayer.Repository DB { get; private set; }
        public QueueHandler QueueHandler { get; set; }
        public int LoggedInUserID
        {
            get
            {
                int result = -1;
                if (Session["UserID"] != null)
                {
                    result = (int)Session["UserID"];
                }
                return result;
            }

            set
            {
                Session["UserID"] = value;
            }
        }



        public BusinessObject.User LoggedInUser
        {
            get
            {
                return DB.UserGet(LoggedInUserID);
            }
            set
            {
                if (value == null)
                {
                    LoggedInUserID = -1;
                }
                else
                {
                    LoggedInUserID = value.Id;
                }
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            DB = new DataLayer.Repository();
            QueueHandler = new QueueHandler(DB);

            base.OnPreInit(e);
        }

        public override void Dispose()
        {
            if (DB != null)
            {
                DB.Dispose();
            }

            base.Dispose();
        }
    }
}