using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTool.Framework.Data
{
    public class TaskRepository : UnconvertedGeneralRepository<DBModels.Task>
    {
        public TaskRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
