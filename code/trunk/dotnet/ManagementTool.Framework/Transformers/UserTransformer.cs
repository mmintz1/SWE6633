using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Framework.Transformers
{
    public class UserTransformer
    {
        public CSUser Transform(User user)
        {
            var model = new CSUser
            {
                CompanyId = user.CompanyId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + " " + user.LastName,
                Role = (Roles)Enum.Parse(typeof(Roles), user.Role)
            };

            return model;
        }

        public List<CSUser> Transform(IEnumerable<User> dbModel)
        {
            var users = new List<CSUser>();
            foreach (var user in dbModel)
            {
                users.Add(Transform(user));
            }

            return users;
        }
    }
}
