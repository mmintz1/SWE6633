using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Risk;

namespace ManagementTool.Website.Controllers
{
    [Authorize]
    public class RiskController : Controller
    {

        public ActionResult Index(int id)
        {
            var mediator = new RiskMediator();
            var model = mediator.GetProjectRisks(id);
            ViewBag.ProjectId = id;
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateRisk(int id)
        {
            RiskVM model = new RiskVM();
            model.ProjectId = id;
            ViewBag.ControllerAction = "CreateRisk";
            ViewBag.PageTitle = "Create Risk";
            return View("~/Views/Risk/RiskForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateRisk(RiskVM model)
        {
            var mediator = new RiskMediator();
            var success = mediator.CreateRisk(model);
            if (success)
            {
                string url = string.Format("/risk/index/{0}", model.ProjectId);
                return Redirect(url);
            }
            else
            {
                ViewBag.ControllerAction = "CreateRisk";
                ViewBag.PageTitle = "Create Risk";
                ModelState.AddModelError("ErrorMessage", "Unable to create risk. Please verify input.");
                return View("~/Views/Risk/RiskForm.cshtml", model);
            }
        }

        [HttpGet]
        public ActionResult EditRisk(int id)
        {
            var mediator = new RiskMediator();
            var model = mediator.GetRisk(id);
            ViewBag.ControllerAction = "EditRisk";
            ViewBag.PageTitle = "Edit Risk";
            return View("~/Views/Risk/RiskForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditRisk(RiskVM model)
        {
            var mediator = new RiskMediator();
            var success = mediator.UpdateRisk(model);
            if (success)
            {
                string url = string.Format("/risk/index/{0}", model.ProjectId);
                return Redirect(url);
            }
            else
            {
                ViewBag.ControllerAction = "CreateRisk";
                ViewBag.PageTitle = "Create Risk";
                ModelState.AddModelError("ErrorMessage", "Unable to update risk.");
                return View("~/Views/Risk/RiskForm.cshtml", model);
            }
        }

        public ActionResult RiskDetails(int id)
        {
            var model = new RiskVM();
            var mediator = new RiskMediator();
            model = mediator.GetRisk(id);
            return View("~/Views/Risk/RiskDetail.cshtml", model);
        }

    }
}
