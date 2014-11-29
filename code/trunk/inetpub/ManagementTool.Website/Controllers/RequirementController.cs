using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Requirement;

namespace ManagementTool.Website.Controllers
{
    [Authorize]
    public class RequirementController : Controller
    {

        public ActionResult Index(int id)
        {
            var mediator = new RequirementMediator();
            var model = mediator.GetProjectRequirements(id);
            ViewBag.ProjectId = id;
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateRequirement(int id)
        {
            RequirementVM model = new RequirementVM();
            model.ProjectId = id;
            ViewBag.ControllerAction = "CreateRequirement";
            ViewBag.PageTitle = "Create Requirement";
            return View("~/Views/Requirement/RequirementForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            var success = mediator.CreateRequirement(model);
            if (success)
            {
                string url = string.Format("/requirement/index/{0}", model.ProjectId);
                return Redirect(url);
            }
            else
            {
                ViewBag.ControllerAction = "CreateRequirement";
                ViewBag.PageTitle = "Create Requirement";
                ModelState.AddModelError("ErrorMessage", "Unable to create requirement. Please verify input.");
                return View("~/Views/Requirement/RequirementForm.cshtml", model);
            }
        }

        [HttpGet]
        public ActionResult EditRequirement(int id)
        {
            var mediator = new RequirementMediator();
            var model = mediator.GetRequirement(id);
            ViewBag.ControllerAction = "EditRequirement";
            ViewBag.PageTitle = "Edit Requirement";
            return View("~/Views/Requirement/RequirementForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            var success = mediator.UpdateRequirement(model);
            if (success)
            {
                string url = string.Format("/requirement/index/{0}", model.ProjectId);
                return Redirect(url);
            }
            else
            {
                ViewBag.ControllerAction = "CreateRequirement";
                ViewBag.PageTitle = "Create Requirement";
                ModelState.AddModelError("ErrorMessage", "Unable to update requirement.");
                return View("~/Views/Requirement/RequirementForm.cshtml", model);
            }
        }

        public ActionResult RequirementDetails(int id)
        {
            var model = new RequirementVM();
            var mediator = new RequirementMediator();
            model = mediator.GetRequirement(id);
            return View("~/Views/Requirement/RequirementDetail.cshtml", model);
        }

    }
}
