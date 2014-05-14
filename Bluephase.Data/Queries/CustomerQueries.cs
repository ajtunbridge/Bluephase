#region Using directives

using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using Bluephase.Data.Model;

#endregion

namespace Bluephase.Data.Queries
{
    public class GetAllCustomersQuery : QueryObjectBase
    {
        public GetAllCustomersQuery()
        {
        }

        public GetAllCustomersQuery(BluephaseEntities entities) : base(entities)
        {
        }

        public IEnumerable<Customer> Execute()
        {
            return Entities.Customers
                .OrderBy(c => c.Name)
                .ToList();
        }
    }

    public class FindCustomerByIdQuery : QueryObjectBase
    {
        public FindCustomerByIdQuery()
        {
        }

        public FindCustomerByIdQuery(BluephaseEntities entities)
            : base(entities)
        {
        }

        public Customer Execute(int customerId)
        {
            return Entities.Customers.Single(c => c.CustomerId == customerId);
        }
    }

    public class FindCustomerByNameQuery : QueryObjectBase
    {
        public FindCustomerByNameQuery()
        {
        }

        public FindCustomerByNameQuery(BluephaseEntities entities) : base(entities)
        {
        }

        public IEnumerable<Customer> Execute(string queryPattern)
        {
            return Entities.Customers
                .Where(c => SqlFunctions.PatIndex(queryPattern, c.Name) > 0)
                .OrderBy(c => c.Name)
                .ToList();
            ;
        }
    }
}