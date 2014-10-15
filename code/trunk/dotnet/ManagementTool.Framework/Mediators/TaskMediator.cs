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
        public void CreateTask(TaskVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);

                var task = new DBModels.Task
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    ProjectId = 1,
                    ExpendedHours = 0,
                    Category = model.Category

                };

                resp.Insert(task);
                var success = db.SaveChanges();
            }
        }
    }
}
