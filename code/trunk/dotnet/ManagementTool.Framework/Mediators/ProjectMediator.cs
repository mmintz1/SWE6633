using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Models.Project;

namespace ManagementTool.Framework.Mediators
{
    public class ProjectMediator
    {
        public void CreateProject(ProjectVM project)
        {
            var db = new ManagementToolEntities();
            var resp = new ProjectRepository(db);
        }
    }
}
