using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Enums;

namespace ManagementTool.Framework.Models.Requirement
{
    public class RequirementVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RequirementCategory Category { get; set; }
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
