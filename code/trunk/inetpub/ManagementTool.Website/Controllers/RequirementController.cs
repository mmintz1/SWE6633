﻿using System;
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
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateRequirement(int id)
        {
            RequirementVM model = new RequirementVM();
            model.ProjectId = id;
            ViewBag.ControllerAction = "CreateRequirement";
            return View("~/Views/Requirement/RequirementForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            mediator.CreateRequirement(model);
            string url = string.Format("/requirement/index/{0}", model.ProjectId);
            return Redirect(url);
        }

        public ActionResult EditRequirement(int id)
        {
            var mediator = new RequirementMediator();
            var model = mediator.GetRequirement(id);
            ViewBag.ControllerAction = "EditRequirement";
            return View("~/Views/Requirement/RequirementForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            mediator.UpdateRequirement(model);
            string url = string.Format("/requirement/index/{0}", model.ProjectId);
            return Redirect(url);
        }

    }
}
