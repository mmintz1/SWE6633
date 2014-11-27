using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Helpers;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Task;

namespace ManagementTool.Website.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        public ActionResult Index(int id)
        {
            var mediator = new TaskMediator();
            var projects = mediator.GetAllTasks(id);
            return View(projects);
        }

        [HttpGet]
        public ActionResult CreateTask(int id)
        {
            var taskModel = new TaskVM();
            taskModel.ProjectId = id;
            ViewBag.ControllerAction = "CreateTask";
            ViewBag.PageTitle = "Create Task";
            var uMediator = new UserMediator();
            taskModel.CompanyEmployees = uMediator.GetUsersByCompanyId();
            return View("~/Views/Task/TaskForm.cshtml", taskModel);
        }

        [HttpPost]
        public ActionResult CreateTask(TaskVM task)
        {
            var mediator = new TaskMediator();

            var success = mediator.CreateTask(task);
            if (success)
            {
                var url = string.Format("/task/index/{0}", task.ProjectId);
                return Redirect(url);
            }
            else
            {
                var uMediator = new UserMediator();
                task.CompanyEmployees = uMediator.GetUsersByCompanyId();
                ViewBag.ControllerAction = "CreateTask";
                ViewBag.PageTitle = "Create Task";
                ModelState.AddModelError("ErrorMessage", "Unable to create task");
                return View("~/Views/Task/TaskForm.cshtml", task);
            }
        }

        [HttpGet]
        public ActionResult EditTask(int id)
        {
            var mediator = new TaskMediator();
            ViewBag.ControllerAction = "EditTask";
            ViewBag.PageTitle = "Edit Task";
            TaskVM taskModel = mediator.GetTask(id);
            var uMediator = new UserMediator();
            
            taskModel.CompanyEmployees = uMediator.GetUsersByCompanyId();
            return View("~/Views/Task/TaskForm.cshtml", taskModel);
        }

        [HttpPost]
        public ActionResult EditTask(TaskVM model)
        {
            var mediator = new TaskMediator();
            bool success = mediator.UpdateTask(model);
            if (success)
            {
                var url = string.Format("/task/index/{0}", model.ProjectId);
                return Redirect(url);
            }
            else
            {
                var uMediator = new UserMediator();

                model.CompanyEmployees = uMediator.GetUsersByCompanyId();
                ViewBag.ControllerAction = "EditTask";
                ViewBag.PageTitle = "Edit Task";
                ModelState.AddModelError("ErrorMessage", "Unable to update Task");
                return View("~/Views/Task/TaskForm.cshtml", model);
            }
        }

        public ActionResult TaskDetails(int id)
        {
            var model = new TaskVM();
            var mediator = new TaskMediator();
            model = mediator.GetTask(id);
            return View("~/Views/Task/TaskDetail.cshtml", model);
        }

    }
}
