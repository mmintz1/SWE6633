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
        public bool CreateProject(ProjectVM model)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new ProjectRepository(db);

                var project = new Project
                {
                    Title = model.Title,
                    Description = model.Description,
                    ProjectManager = model.Manager,
                    Status = model.Status.ToString(),
                    DueDate = DateTime.Parse(model.DueDate),
                    CompanyId = model.CompanyId,
                    TeamMembers = string.Join(";", model.ProjectEmployees)
                };

                resp.Insert(project);
                var success = db.SaveChanges() > 0;

                return success;
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

        public ProjectVM GetProject(int id)
        {
            var model = new ProjectVM();
            var transformer = new ProjectTransformer();

            using (var db = new ManagementToolEntities())
            {
                var resp = new ProjectRepository(db);

                Project project = resp.GetFirstOrDefault(p => p.ProjectID == id);
                model = transformer.Transform(project);
            }

            return model;
        }

        public bool UpdateProject(ProjectVM model)
        {
            bool success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new ProjectRepository(db);
                Project dbProject = resp.GetFirstOrDefault(p => p.ProjectID == model.Id);

                dbProject.Description = model.Description;
                dbProject.DueDate = DateTime.Parse(model.DueDate);
                dbProject.ProjectManager = model.Manager;
                dbProject.Status = model.Status.ToString();
                dbProject.Title = model.Title;

                resp.Update(dbProject);
                success = db.SaveChanges() > 0;
            }

            return success;
        }
    }
}
