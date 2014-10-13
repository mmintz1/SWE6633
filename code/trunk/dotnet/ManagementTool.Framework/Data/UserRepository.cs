using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.DBModels;

namespace ManagementTool.Framework.Data
{
    public class UserRepository : UnconvertedGenericRepository<DBModels.User>
    {
        public UserRepository(UnitOfWork context, DbContext dbContext) : base(context, dbContext)
        {
        }

        public User GetUser(string email)
        {
            return GetFirstOrDefault(user => user.Email == email);
        }
    }
}
