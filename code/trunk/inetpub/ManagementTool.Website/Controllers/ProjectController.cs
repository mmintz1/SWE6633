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
            return View(mine);
        }

        public ActionResult CreateProject()
        {
            var projectModel = new ProjectVM();
            ViewBag.ControllerAction = "AddProject";
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }
        
        [HttpPost]
        public ActionResult AddProject(ProjectVM project)
        {
            var mediator = new ProjectMediator();
            mediator.CreateProject(project);
            return Redirect("/");
        }

        public ActionResult EditProject(int id)
        {
            var projectModel = new ProjectVM();
            ViewBag.ControllerAction = "UpdateProject";
            var mediator = new ProjectMediator();
            projectModel = mediator.GetProject(id);
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }

        [HttpPost]
        public ActionResult UpdateProject(ProjectVM model)
        {
            var mediator = new ProjectMediator();
            mediator.UpdateProject(model);
            return Redirect("/");
        }
    }
}
