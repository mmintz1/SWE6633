﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Framework.Models.Task
{
    public class TaskVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string DueDate { get; set; }
        public string Category { get; set; }
        public Status Status { get; set; }
        public double TaskHours { get; set; }
        public int ProjectId { get; set; }
        public List<CSUser> CompanyEmployees { get; set; }
        public string Developer { get; set; }
    }
}
