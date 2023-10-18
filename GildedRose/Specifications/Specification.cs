using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Specifications
{
    public abstract class Specification<T> // Where T is an entity, TODO: Define entity base class
    {
        protected abstract Expression<Func<T, bool>> Expression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = this.Expression().Compile();
            return predicate(entity);
        }
    }
}
