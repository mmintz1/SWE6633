using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Helpers;
using ManagementTool.Framework.Models.Account;
using ManagementTool.Framework.Transformers;

namespace ManagementTool.Framework.Mediators
{
    public class UserMediator
    {
        public bool RegisterUser(RegisterVM reg)
        {
            var success = false;
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);

                bool emailExist = resp.AccountExist(reg.Email);

                if (!emailExist)
                {
                    var regUser = new User
                    {
                        Email = reg.Email.Trim().ToLower(),
                        FirstName = reg.FirstName,
                        LastName = reg.LastName,
                        Password = reg.Password,
                        CompanyId = reg.CompanyId,
                        Role = Roles.Employee.ToString()
                    };
                    resp.Insert(regUser);
                    success = db.SaveChanges() > 0;
                }
            }

            return success;
        }

        public bool Authenticate(LoginVM user)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);

                bool isAuthenticated = false;

                var account = resp.GetFirstOrDefault(u => u.Email.Trim().ToLower() == user.Email.Trim().ToLower());

                if (account != null)
                {
                    if (user.Password == account.Password)
                    {
                        isAuthenticated = true;
                    }
                }

                return isAuthenticated;
            }

        }

        public CSUser GetUser(string email)
        {
            CSUser user = null;
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);
                var dbUser = resp.GetFirstOrDefault(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

                UserTransformer transformer = new UserTransformer();
                user = transformer.Transform(dbUser);

            }
            return user;
        }

        public List<CSUser> GetUsersByCompanyId(int companyId)
        {
            List<CSUser> users = null;
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);
                var dbUsers = resp.GetAllUsersInCompany(companyId);
                if (dbUsers != null && dbUsers.Count() > 0)
                {
                    UserTransformer transformer = new UserTransformer();
                    users = transformer.Transform(dbUsers);
                }
            }

            return users;
        }

        public List<CSUser> GetUsersByCompanyId()
        {
            var user = SessionHelper.GetUserSession();
            return GetUsersByCompanyId(user.CompanyId);
        }
    }
}
