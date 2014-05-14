using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluephase.Data.Model;

namespace Bluephase.Data.Queries
{
    public class FindPartByIdQuery : QueryObjectBase
    {
        public FindPartByIdQuery()
        {
        }

        public FindPartByIdQuery(BluephaseEntities entities) : base(entities)
        {
        }

        public Part Execute(int partId)
        {
            return Entities.Parts.Single(p => p.PartId == partId);
        }
    }

    public class FindChildPartsQuery : QueryObjectBase
    {
        public FindChildPartsQuery()
        {
        }

        public FindChildPartsQuery(BluephaseEntities entities) : base(entities)
        {

        }

        public IEnumerable<Part> Execute(int parentPartId)
        {
            return Entities.Parts
                .Where(p => p.AssemblyPartId == parentPartId)
                .ToList();
        }
    }
}