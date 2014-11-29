using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTool.Framework.Data
{
    public class RiskRepository : UnconvertedGeneralRepository<DBModels.Risk>
    {
        public RiskRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
