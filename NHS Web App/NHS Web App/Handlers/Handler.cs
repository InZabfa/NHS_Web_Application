using BusinessObject;
using DataLayer;
using NHS_Web_App.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHS_Web_App.Handlers
{
    public class Handler
    {
        public static void HandleAuthorisation(Repository DB, BasePage @base, User LoggedInUser, HttpResponse Response, List<BasePage.Permissions> permissions, String currentPath)
        {
            if (DB.HasAdmin())
            {
                if (LoggedInUser == null)
                {
                    Response.Redirect(Helper.PageAddress(Helper.Pages.LOGIN) + "?return=" + currentPath);
                }
                // Check if the user has the acceptable permissions of the page they are accessing
                else if (!permissions.All(i => @base.GetPermission().Contains(i)))
                {
                    throw new HttpException(403, GlobalVariables.MESSAGE_NOTAUTHORISED);
                }
                else if (!LoggedInUser.Access_Levels.Access_Enabled)
                {
                    Response.Redirect(Helper.PageAddress(Helper.Pages.DISABLED_ACCOUNT));
                }
            }
            else
            {
                Response.Redirect(Helper.PageAddress(Helper.Pages.WELCOME));
            }
        }
    }
}