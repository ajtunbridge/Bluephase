#region Using directives

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

#endregion

namespace Bluephase.Data.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly BluephaseUnitOfWork UnitOfWork;
        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(BluephaseUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _dbSet = UnitOfWork.Entities.Set<T>();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            UnitOfWork.Entities.Set<T>().Attach(entity);

            DbEntityEntry<T> entry = UnitOfWork.Entities.Entry(entity);

            entry.State = EntityState.Modified;

            // UnitOfWork.EntitiesToDetach.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Attach(entity);

            DbEntityEntry<T> entry = UnitOfWork.Entities.Entry(entity);

            entry.State = EntityState.Deleted;
        }

        public IEnumerable<T> SelectAll()
        {
            return _dbSet;
        }

        protected IEnumerable<T> SelectWhere(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}