using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace ManagementTool.Framework.Data
{
    public class UnconvertedGenericRepository<TEntity> : GenericRepository<TEntity, TEntity>
        where TEntity : class
    {
        public UnconvertedGenericRepository(UnitOfWork context, DbContext dbContext)
            : base(context, dbContext)
        {

        }

        protected override TEntity Convert(TEntity model)
        {
            return model;
        }
    }

    public abstract class GenericRepository<TDomain, TEntity>
        where TDomain : class
        where TEntity : class
    {
        protected UnitOfWork context;
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(UnitOfWork context, DbContext dbContext)
        {
            this.context = context;
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TDomain> GetAll(
            string includeProperties = "")
        {
            IQueryable<TEntity> models = dbSet.AsExpandable();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                models = models.Include(includeProperty);
            }
            if (models != null && models.Count() > 0)
            {
                return Convert(models);
            }
            return null;
        }

        public IEnumerable<TDomain> GetAll()
        {
            return GetAll(string.Empty);
        }

        public virtual TDomain Get(int id)
        {
            TEntity model = dbSet.Find(id);
            if (model != null)
            {
                return Convert(model);
            }
            return null;
        }

        public virtual TDomain Get(TEntity model)
        {
            return Convert(model);
        }

        //this is faster than doing a where(predicate).FirstOrDefault()
        public virtual TDomain GetFirstOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
            {
                return Convert(query.FirstOrDefault(filter));
            }
            return Convert(query.FirstOrDefault());

        }

        public virtual IEnumerable<TDomain> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet.AsExpandable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return Convert(orderBy(query));
            }
            else
            {
                return Convert(query);
            }
        }

        public virtual TEntity Insert(TEntity model)
        {
            return dbSet.Add(model);
        }

        public virtual TEntity Delete(int id)
        {
            var model = dbSet.Find(id);
            return Delete(model);
        }

        public virtual TEntity Delete(TEntity model)
        {
            CheckModelState(model);
            return dbSet.Remove(model);
        }

        public virtual TEntity Update(TEntity model)
        {
            dbSet.Attach(model);
            dbContext.Entry(model).State = EntityState.Modified;
            return model;
        }

        protected void CheckModelState(TEntity model)
        {
            if (dbContext.Entry(model).State == EntityState.Detached)
            {
                dbSet.Attach(model);
            }
        }

        #region Methods to convert from model to domain class

        protected virtual IEnumerable<TDomain> Convert(IEnumerable<TEntity> models)
        {
            return models.AsEnumerable().Select(model => Convert(model));
        }

        protected abstract TDomain Convert(TEntity model);

        #endregion
    }
}
