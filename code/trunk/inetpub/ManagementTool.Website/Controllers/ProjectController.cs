using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Project;

namespace ManagementTool.Website.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProject()
        {
            var projectModel = new ProjectVM();
            return View("~/Views/Project/CreateProject.cshtml", projectModel);
        }

        public ActionResult AddProject(ProjectVM project)
        {
            var mediator = new ProjectMediator();
            return Redirect("/");
        }
    }
}
