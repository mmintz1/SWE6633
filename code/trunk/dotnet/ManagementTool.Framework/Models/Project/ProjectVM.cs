using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Account;


namespace ManagementTool.Framework.Models.Project
{
    public class ProjectVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string DueDate { get; set; }
        public int Id { get; set; }
        public string Manager { get; set; }
        public int CompanyId { get; set; }
        public List<CSUser> CompanyEmployees { get; set; }
        public string[] ProjectEmployees { get; set; }
    }
}
