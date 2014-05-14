using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluephase.Data.Model;

namespace Bluephase.Data.Queries
{
    public abstract class QueryObjectBase
    {
        protected readonly BluephaseEntities Entities;

        protected QueryObjectBase() : this(new BluephaseEntities())
        {

        }

        protected QueryObjectBase(BluephaseEntities entities)
        {
            Entities = entities;
        }
    }
}