using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Requirement;

namespace ManagementTool.Framework.Mediators
{
    public class RequirementMediator
    {
        public void CreateRequirement(RequirementVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new RequirementRepository(db);

                var requirement = new Requirement
                {
                    Title = model.Title,
                    Description = model.Description,
                    ProjectId = 1,
                    Type = model.Category.ToString()
                };

                resp.Insert(requirement);
                var success = db.SaveChanges();
            }
        }
    }
}
