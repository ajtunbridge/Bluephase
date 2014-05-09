#region Using directives

using System.Linq;
using Bluephase.Data.Model;

#endregion

namespace Bluephase.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(BluephaseUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Customer Select(int customerId)
        {
            return SelectWhere(c => c.CustomerId == customerId).Single();
        }
    }
}