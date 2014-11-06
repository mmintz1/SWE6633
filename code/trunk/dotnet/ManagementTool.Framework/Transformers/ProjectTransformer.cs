using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Project;

namespace ManagementTool.Framework.Transformers
{
    public class ProjectTransformer
    {
        public ProjectVM Transform(Project model)
        {
            ProjectVM project = new ProjectVM();
            if (model != null)
            {
                project.Description = model.Description;
                project.DueDate = model.DueDate;
                project.Id = (int)model.ProjectID;
                project.Manager = model.ProjectManager;
                project.Status = (Status)Enum.Parse(typeof(Status), model.Status);
                project.Title = model.Title;
                project.CompanyId = model.CompanyId;
            }

            return project;
        }

        public List<ProjectVM> Transform(IEnumerable<Project> model)
        {
            List<ProjectVM> projects = new List<ProjectVM>();

            foreach (var project in model)
            {
                projects.Add(Transform(project));
            }
            return projects;
        }
    }
}
