using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Requirement;

namespace ManagementTool.Framework.Transformers
{
    public class RequirementTransformer
    {
        public RequirementVM Transform(DBModels.Requirement requirement)
        {
            var model = new RequirementVM
            {
                Title = requirement.Title,
                Description = requirement.Description,
                Category = (RequirementCategory)Enum.Parse(typeof(RequirementCategory), requirement.Category),
                Type = (RequirementType)Enum.Parse(typeof(RequirementType), requirement.Type),
                ProjectId = requirement.ProjectId,
                Id = requirement.RequirementId
            };

            return model;
        }

        public List<RequirementVM> Transform(IEnumerable<DBModels.Requirement> requirements)
        {
            var model = new List<RequirementVM>();
            foreach (var req in requirements)
            {
                model.Add(Transform(req));
            }

            return model;
        }
    }
}
