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
                    Status = model.Status.ToString(),
                    ProjectId = 1,
                    ExpendedHours = 0,
                    Category = model.Category

                };

                resp.Insert(task);
                var success = db.SaveChanges() > 0;
            }
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

        public void UpdateTask(TaskVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new TaskRepository(db);
                DBModels.Task dbTask = resp.GetFirstOrDefault(t => t.TaskID == model.Id);

                dbTask.Category = model.Category;
                dbTask.Description = model.Description;
                dbTask.DueDate = model.DueDate;
                dbTask.ExpendedHours += model.TaskHours;
                dbTask.Status = model.Status.ToString();
                dbTask.Title = model.Title;

                resp.Update(dbTask);
                var success = db.SaveChanges() > 0;

            }
        }
    }
}
