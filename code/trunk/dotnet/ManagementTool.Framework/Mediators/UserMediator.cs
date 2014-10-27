using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.Data;
using ManagementTool.Framework.DBModels;
using ManagementTool.Framework.Enums;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Framework.Mediators
{
    public class UserMediator
    {
        public void RegisterUser(RegisterVM reg)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);

                bool emailExist = resp.AccountExist(reg.Email);

                if (!emailExist)
                {
                    var regUser = new User
                    {
                        Email = reg.Email,
                        FirstName = reg.FirstName,
                        LastName = reg.LastName,
                        Password = reg.Password,
                        CompanyId = reg.CompanyId,
                        Role = Roles.Employee.ToString()
                    };
                    var mine = resp.Insert(regUser);
                    var success = db.SaveChanges() > 0;
                }
            }
        }

        public bool Authenticate(LoginVM user)
        {
            using (var db = new ManagementToolEntities())
            {
                var resp = new UsersRepository(db);

                bool isAuthenticated = false;

                var account = resp.GetFirstOrDefault(u => u.Email == user.Email);

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
    }
}
