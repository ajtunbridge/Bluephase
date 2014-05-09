#region Using directives

using System;
using Bluephase.Data.Model;
using Bluephase.Data.Repositories;

#endregion

namespace Bluephase.Data
{
    public class BluephaseUnitOfWork : IDisposable
    {
        private readonly BluephaseEntities _entities;
        private CustomerRepository _customers;

        public BluephaseUnitOfWork()
        {
            _entities = new BluephaseEntities();
        }

        internal BluephaseEntities Entities
        {
            get { return _entities; }
        }

        public CustomerRepository Customers
        {
            get { return (_customers ?? new CustomerRepository(this)); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                Entities.Dispose();
            }
        }
    }
}