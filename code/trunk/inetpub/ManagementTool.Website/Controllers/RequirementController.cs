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

        [HttpPost]
        public ActionResult AddRequirement(RequirementVM model)
        {
            var mediator = new RequirementMediator();
            mediator.CreateRequirement(model);

            return View();
        }

    }
}
