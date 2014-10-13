using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementTool.Framework.DBModels;

namespace ManagementTool.Framework.Data
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;

        private readonly Lazy<ManagementToolEntities> managementToolContext = new Lazy<ManagementToolEntities>();

        public ManagementToolEntities ManagementToolContext
        {
            get { return managementToolContext.Value; }
        }
        private readonly Lazy<UserRepository> user;

        public UnitOfWork()
        {
            user = new Lazy<UserRepository>(() => new UserRepository(this, managementToolContext.Value));
        }

        public UserRepository User
        {
            get { return user.Value; }
        }

        public int Save()
        {
            int changes = 0;

            if (managementToolContext.IsValueCreated)
            {
                changes += managementToolContext.Value.SaveChanges();
            }

            return changes;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (managementToolContext.IsValueCreated)
                    {
                        managementToolContext.Value.Dispose();
                    }
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
