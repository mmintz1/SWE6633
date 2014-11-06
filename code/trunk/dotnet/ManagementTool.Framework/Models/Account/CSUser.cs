using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Enums;

namespace ManagementTool.Framework.Models.Account
{
    public class CSUser
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int CompanyId { get; set; }
        public Roles Role { get; set; }
    }
}
