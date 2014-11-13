using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Requirement;
using ManagementTool.Framework.Transformers;

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
                var success = db.SaveChanges() > 0;
            }
        }

        public List<RequirementVM> GetProjectRequirements(int projectId)
        {
            var model = new List<RequirementVM>();
            var transformer = new RequirementTransformer();
            using (var db = new ManagementToolEntities())
            {
                var resp = new RequirementRepository(db);

                IEnumerable<DBModels.Requirement> requirements = resp.Get(r => r.ProjectId == projectId);
                model = transformer.Transform(requirements);
            }

            return model;
        }

        public RequirementVM GetRequirement(int requirementId)
        {
            var model = new RequirementVM();
            var transformer = new RequirementTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new RequirementRepository(db);

                var requirement = resp.GetFirstOrDefault(r => r.RequirementId == requirementId);
                model = transformer.Transform(requirement);
            }

            return model;
        }

        public void UpdateRequirement(RequirementVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new RequirementRepository(db);

                var requirement = resp.GetFirstOrDefault(r => r.RequirementId == model.Id);

                requirement.Type = model.Category.ToString();
                requirement.Description = model.Description;
                requirement.Title = model.Title;

                resp.Update(requirement);
                var success = db.SaveChanges() > 0;
            }
        }
    }
}
