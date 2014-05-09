#region Using directives

using System.Linq;
using Bluephase.Data.Model;

#endregion

namespace Bluephase.Data.Repositories
{
    public sealed class PersonRepository : RepositoryBase<Person>
    {
        public PersonRepository(BluephaseUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Person Select(int personId)
        {
            return SelectWhere(p => p.PersonId == personId).Single();
        }
    }
}