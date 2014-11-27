using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Task;

namespace ManagementTool.Framework.Transformers
{
    public class TaskTransformer
    {
        public TaskVM Transform(DBModels.Task model)
        {
            TaskVM task = new TaskVM();
            if (model != null)
            {
                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate.ToString("MM/dd/yyyy");
                task.Id = model.TaskID;
                task.Status = (Status)Enum.Parse(typeof(Status), model.Status);
                task.ProjectId = model.ProjectId;
                task.TaskHours = model.ExpendedHours;
                task.Category = model.Category;
                task.Developer = model.AssignedTo;
            }

            return task;
        }

        public List<TaskVM> Transform(IEnumerable<DBModels.Task> model)
        {
            List<TaskVM> tasks = new List<TaskVM>();

            foreach (var task in model)
            {
                tasks.Add(Transform(task));
            }
            return tasks;
        }
    }
}
