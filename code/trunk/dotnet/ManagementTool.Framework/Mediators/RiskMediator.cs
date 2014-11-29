using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Risk;
using ManagementTool.Framework.Transformers;

namespace ManagementTool.Framework.Mediators
{
    public class RiskMediator
    {
        public bool CreateRisk(RiskVM model)
        {
            bool success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new RiskRepository(db);

                var risk = new Risk
                {
                    Title = model.Title,
                    Description = model.Description,
                    ProjectId = model.ProjectId,
                    Status = model.Status.ToString()
                };

                resp.Insert(risk);
                success = db.SaveChanges() > 0;
            }

            return success;
        }

        public List<RiskVM> GetProjectRisks(int projectId)
        {
            var model = new List<RiskVM>();
            var transformer = new RiskTransformer();
            using (var db = new ManagementToolEntities())
            {
                var resp = new RiskRepository(db);

                IEnumerable<DBModels.Risk> risks = resp.Get(r => r.ProjectId == projectId);
                model = transformer.Transform(risks);
            }

            return model;
        }

        public RiskVM GetRisk(int riskId)
        {
            var model = new RiskVM();
            var transformer = new RiskTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new RiskRepository(db);

                var risk = resp.GetFirstOrDefault(r => r.RiskId == riskId);
                model = transformer.Transform(risk);
            }

            return model;
        }

        public bool UpdateRisk(RiskVM model)
        {
            bool success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new RiskRepository(db);

                var risk = resp.GetFirstOrDefault(r => r.RiskId == model.Id);

                risk.Status = model.Status.ToString();
                risk.Description = model.Description;
                risk.Title = model.Title;

                resp.Update(risk);
                success = db.SaveChanges() > 0;
            }

            return success;
        }
    }
}
