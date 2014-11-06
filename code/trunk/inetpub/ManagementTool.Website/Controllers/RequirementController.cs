using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Requirement;

namespace ManagementTool.Website.Controllers
{
    public class RequirementController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRequirement(int id)
        {
            RequirementVM model = new RequirementVM();
            model.ProjectId = id;
            return View("~/Views/Requirement/RequirementForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            mediator.CreateRequirement(model);

            return Redirect("/project/index");
        }

    }
}
