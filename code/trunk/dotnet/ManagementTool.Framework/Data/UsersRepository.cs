using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTool.Framework.Data
{
    public class UsersRepository : UnconvertedGeneralRepository<DBModels.User>
    {
        public UsersRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool AccountExist(string email)
        {
            return GetFirstOrDefault(user => user.Email == email) != null;
        }
    }
}
