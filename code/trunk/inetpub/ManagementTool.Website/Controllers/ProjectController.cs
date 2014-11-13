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

            CSUser user =  (CSUser)Session["UserAccount"];
            var projects = mediator.GetAllCompanyProjects(user.CompanyId);
            return View(projects);
        }

        public ActionResult CreateProject(int Id)
        {
            var projectModel = new ProjectVM();
            var uMediator = new UserMediator();
            projectModel.CompanyEmployees = HelperFunctions.CreateUserDropdownList();
            projectModel.CompanyId = Id;
            projectModel.Users = uMediator.GetUsersByCompanyId(Id);
            ViewBag.ControllerAction = "AddProject";
            ViewBag.PageTitle = "Create Project";
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }
        
        [HttpPost]
        public ActionResult AddProject(ProjectVM project)
        {
            var mediator = new ProjectMediator();
            bool success = mediator.CreateProject(project);
            if (success)
                return Redirect("~/project/index");
            else
            {
                ViewBag.ControllerAction = "AddProject";
                ViewBag.PageTitle = "Create Project";
                ModelState.AddModelError("ErrorMessage", "Unable to create project. Please verify input.");
                return View("~/Views/Project/ProjectForm.cshtml", project);
            }
        }

        public ActionResult EditProject(int id)
        {
            var projectModel = new ProjectVM();
            ViewBag.ControllerAction = "UpdateProject";
            ViewBag.PageTitle = "Edit Project";
            var mediator = new ProjectMediator();
            var uMediator = new UserMediator();
            projectModel = mediator.GetProject(id);
            projectModel.CompanyEmployees = HelperFunctions.CreateUserDropdownList();
            projectModel.Users = uMediator.GetUsersByCompanyId(id);
            return View("~/Views/Project/ProjectForm.cshtml", projectModel);
        }

        [HttpPost]
        public ActionResult UpdateProject(ProjectVM model)
        {
            var mediator = new ProjectMediator();
            mediator.UpdateProject(model);
            return Redirect("/project/index");
        }

        public ActionResult ProjectDetails(int id)
        {
            var model = new ProjectVM();
            var mediator = new ProjectMediator();
            model = mediator.GetProject(id);
            return View("~/Views/Project/ProjectDetail.cshtml", model);
        }

        //public List<SelectListItem> CreateUserDropdownList(int id)
        //{
        //    var mediator = new UserMediator();
        //    var users = mediator.GetUsersByCompanyId(id);
        //    var list = new List<SelectListItem>();
        //    foreach (var user in users)
        //    {
        //        list.Add(new SelectListItem { Text = user.FullName, Value = user.FullName });
        //    }

        //    return list;
        //}
    }
}
