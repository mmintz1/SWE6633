using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Enums;

namespace ManagementTool.Framework.Models.Risk
{
    public class RiskVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RiskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
