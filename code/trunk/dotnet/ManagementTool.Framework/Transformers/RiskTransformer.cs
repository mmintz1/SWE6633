using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Risk;

namespace ManagementTool.Framework.Transformers
{
    public class RiskTransformer
    {
        public RiskVM Transform(DBModels.Risk risk)
        {
            var model = new RiskVM
            {
                Title = risk.Title,
                Description = risk.Description,
                Status = (RiskStatus)Enum.Parse(typeof(RiskStatus), risk.Status),
                ProjectId = risk.ProjectId,
                Id = risk.RiskId
            };

            return model;
        }

        public List<RiskVM> Transform(IEnumerable<DBModels.Risk> risks)
        {
            var model = new List<RiskVM>();
            foreach (var risk in risks)
            {
                model.Add(Transform(risk));
            }

            return model;
        }
    }
}
