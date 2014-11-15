using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Helpers;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Account;
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
            var mediator = new ProjectMediator();
            CSUser user = SessionHelper.GetUserSession();
            var projects = mediator.GetAllCompanyProjects(user.CompanyId);
            return View(projects);
        }

        [HttpGet]
        public ActionResult CreateProject(int Id)
        {
            var projectModel = new ProjectVM();
            var uMediator = new UserMediator();
            projectModel.CompanyEmployees = uMediator.GetUsersByCompanyId(Id);
            projectModel.CompanyId = Id;
            ViewBag.ControllerAction = "CreateProject";
            ViewBag.PageTitle = "Create Project";
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }
        
        [HttpPost]
        public ActionResult CreateProject(ProjectVM project)
        {
            var mediator = new ProjectMediator();
            var uMediator = new UserMediator();
            if (string.IsNullOrWhiteSpace(project.DueDate))
            {
                ViewBag.ControllerAction = "CreateProject";
                ViewBag.PageTitle = "Create Project";
                ModelState.AddModelError("ErrorMessage", "Please select a Due Date");
                project.CompanyEmployees = uMediator.GetUsersByCompanyId(project.CompanyId);
                return View("~/Views/Project/ProjectForm.cshtml", project);
            }
            bool success = mediator.CreateProject(project);
            if (success)
                return Redirect("~/project/index");
            else
            {
                ViewBag.ControllerAction = "CreateProject";
                ViewBag.PageTitle = "Create Project";
                ModelState.AddModelError("ErrorMessage", "Unable to create project. Please verify input.");
                project.CompanyEmployees = uMediator.GetUsersByCompanyId(project.CompanyId);
                return View("~/Views/Project/ProjectForm.cshtml", project);
            }
        }

        [HttpGet]
        public ActionResult EditProject(int id)
        {
            var projectModel = new ProjectVM();
            ViewBag.ControllerAction = "EditProject";
            ViewBag.PageTitle = "Edit Project";
            var mediator = new ProjectMediator();
            var uMediator = new UserMediator();
            projectModel = mediator.GetProject(id);
            
            projectModel.CompanyEmployees = uMediator.GetUsersByCompanyId(projectModel.CompanyId);
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }

        [HttpPost]
        public ActionResult EditProject(ProjectVM model)
        {
            var mediator = new ProjectMediator();
            bool success = mediator.UpdateProject(model);
            if (success)
                return Redirect("/project/index");
            else
            {
                ModelState.AddModelError("ErrorMessage", "Unable to update project");
                var uMediator = new UserMediator();
                model.CompanyEmployees = uMediator.GetUsersByCompanyId(model.CompanyId);
                return View("~/Views/Project/ProjectForm.cshtml", model);
            }
        }

        public ActionResult ProjectDetails(int id)
        {
            var model = new ProjectVM();
            var mediator = new ProjectMediator();
            model = mediator.GetProject(id);
            return View("~/Views/Project/ProjectDetail.cshtml", model);
        }
    }
}
