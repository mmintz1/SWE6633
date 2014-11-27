using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Task;
using ManagementTool.Framework.Transformers;

namespace ManagementTool.Framework.Mediators
{
    public class TaskMediator
    {
        public bool CreateTask(TaskVM model)
        {
            bool success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);

                var task = new DBModels.Task
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = DateTime.Parse(model.DueDate),
                    Status = model.Status.ToString(),
                    ProjectId = model.ProjectId,
                    ExpendedHours = model.TaskHours,
                    Category = model.Category,
                    AssignedTo = model.Developer
                };

                resp.Insert(task);
                success = db.SaveChanges() > 0;
            }

            return success;
        }

        public List<TaskVM> GetAllTasks(int projectId)
        {
            var tasks = new List<TaskVM>();
            var transformer = new TaskTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);
                IEnumerable<DBModels.Task> dbtasks = resp.Get(t => t.ProjectId == projectId);
                tasks = transformer.Transform(dbtasks);
            }
            return tasks;
        }

        public TaskVM GetTask(int id)
        {
            var model = new TaskVM();
            var transformer = new TaskTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);

                DBModels.Task task = resp.GetFirstOrDefault(t => t.ProjectId == id);
                model = transformer.Transform(task);
            }

            return model;
        }

        public bool UpdateTask(TaskVM model)
        {
            bool success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);
                DBModels.Task dbTask = resp.GetFirstOrDefault(t => t.TaskID == model.Id);

                dbTask.Category = model.Category;
                dbTask.Description = model.Description;
                dbTask.DueDate = DateTime.Parse(model.DueDate);
                dbTask.ExpendedHours = model.TaskHours;
                dbTask.Status = model.Status.ToString();
                dbTask.Title = model.Title;
                dbTask.AssignedTo = model.Developer;

                resp.Update(dbTask);
                success = db.SaveChanges() > 0;

            }

            return success;
        }
    }
}
