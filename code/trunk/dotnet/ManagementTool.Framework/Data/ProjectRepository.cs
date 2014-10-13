using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTool.Framework.Data
{
    public class ProjectRepository : UnconvertedGeneralRepository<DBModels.Project>
    {
        public ProjectRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
