using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Task;

namespace ManagementTool.Framework.Mediators
{
    public class TaskMediator
    {
        public void CreateTask(TaskVM task)
        {
            var db = new ManagementToolEntities();
            var resp = new TaskRepository(db);
        }
    }
}
