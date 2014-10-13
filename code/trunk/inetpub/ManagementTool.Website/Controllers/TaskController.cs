using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Task;

namespace ManagementTool.Website.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTask()
        {
            var taskModel = new TaskVM();
            return View("~/Views/Task/CreateTask.cshtml", taskModel);
        }

        public ActionResult AddTask()
        {
            var mediator = new TaskMediator();
            return Redirect("/");
        }

    }
}
