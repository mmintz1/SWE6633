using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Framework.Helpers
{
    public static class SessionHelper
    {
        public static CSUser GetUserSession()
        {
            CSUser user = null;
            if (HttpContext.Current.Session != null)
            {
                user = (CSUser)HttpContext.Current.Session["CSUser"];
                if (user == null)
                {
                    UserMediator mediator = new UserMediator();
                    return user = mediator.GetUser(HttpContext.Current.User.Identity.Name);
                }
            }

            return user;
        }
    }
}
