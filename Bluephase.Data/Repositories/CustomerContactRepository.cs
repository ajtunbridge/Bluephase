#region Using directives

using System.Collections.Generic;
using System.Linq;
using Bluephase.Data.Model;

#endregion

namespace Bluephase.Data.Repositories
{
    public sealed class CustomerContactRepository : RepositoryBase<CustomerContact>
    {
        public CustomerContactRepository(BluephaseUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CustomerContact Select(int customerId, int personId)
        {
            return SelectWhere(cc => cc.CustomerId == customerId && cc.PersonId == personId).Single();
        }

        public IEnumerable<CustomerContact> GetByCustomer(Customer customer)
        {
            return GetByCustomer(customer.CustomerId);
        }

        public IEnumerable<CustomerContact> GetByCustomer(int customerId)
        {
            return SelectWhere(c => c.CustomerId == customerId);
        }
    }
}