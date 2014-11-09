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

        public ActionResult CreateTask()
        {
            var taskModel = new TaskVM();
            ViewBag.ControllerAction = "AddTask";
            taskModel.Users = HelperFunctions.CreateUserDropdownList();
            return View("~/Views/Task/TaskForm.cshtml", taskModel);
        }

        [HttpPost]
        public ActionResult AddTask(TaskVM task)
        {
            var mediator = new TaskMediator();

            mediator.CreateTask(task);
            return Redirect("/");
        }

        public ActionResult EditTask(int id)
        {
            var mediator = new TaskMediator();
            ViewBag.ControllerAction = "EditTask";
            TaskVM taskModel = mediator.GetTask(id);
            taskModel.Users = HelperFunctions.CreateUserDropdownList();
            return View("~/Views/Task/TaskForm.cshtml", taskModel);
        }

        [HttpPost]
        public ActionResult UpdateTask(TaskVM model)
        {
            var mediator = new TaskMediator();
            mediator.UpdateTask(model);
            return Redirect("/");
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
