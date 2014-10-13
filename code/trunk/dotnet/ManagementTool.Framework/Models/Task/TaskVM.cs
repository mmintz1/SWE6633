using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTool.Framework.Models.Task
{
    public class TaskVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }
    }
}
