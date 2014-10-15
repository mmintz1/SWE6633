using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Project;

namespace ManagementTool.Website.Controllers
{
    //[Authorize]
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            var mediator = new ProjectMediator();
            var mine = mediator.GetAllCompanyProjects(1);
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
            mediator.CreateProject(project);
            return Redirect("/");
        }
    }
}
