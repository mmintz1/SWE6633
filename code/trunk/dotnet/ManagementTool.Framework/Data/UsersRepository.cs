using System;
using System.Collections;
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

        public IEnumerable<DBModels.User> GetAllUsersInCompany(int companyId)
        {
            return Get(users => users.CompanyId == companyId);
        }

        //public IEnumerable<DBModels.User> GetAllUsersInProject(int projectId)
        //{

        //}
    }
}
