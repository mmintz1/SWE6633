using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Project;
using ManagementTool.Framework.Transformers;

namespace ManagementTool.Framework.Mediators
{
    public class ProjectMediator
    {
        public void CreateProject(ProjectVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new ProjectRepository(db);

                var project = new Project
                {
                    Title = model.Title,
                    Description = model.Description,
                    ProjectManager = model.Manager,
                    Status = model.Status,
                    DueDate = model.DueDate,
                    CompanyId = 1
                };

                resp.Insert(project);
                db.SaveChanges();
            }
        }

        public List<ProjectVM> GetAllCompanyProjects(int companyId)
        {
            var projects = new List<ProjectVM>();
            var transformer = new ProjectTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new ProjectRepository(db);
                IEnumerable<Project>  dbProjects = resp.Get(proj => proj.CompanyId == companyId);
                projects = transformer.Transform(dbProjects);
            }
            return projects;
        }
    }
}
