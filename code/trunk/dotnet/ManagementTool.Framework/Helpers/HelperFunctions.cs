using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Framework.Helpers
{
    public static class HelperFunctions
    {
        public static List<SelectListItem> CreateUserDropdownList()
        {
            CSUser csUser = (CSUser)HttpContext.Current.Session["UserAccount"];
            
            var mediator = new UserMediator();
            var users = mediator.GetUsersByCompanyId(csUser.CompanyId);
            var list = new List<SelectListItem>();
            foreach (var user in users)
            {
                list.Add(new SelectListItem { Text = user.FullName, Value = user.FullName });
            }

            return list;
        }
    }
}
