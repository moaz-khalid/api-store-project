using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Store.Core.Specifications
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria  { get; set; }   // Filter condition (where clause)

        public List<Expression<Func<TEntity,object>>> Include { get; set; } // List of navigation properties to include in the query

        public Expression<Func<TEntity, object>> OrderBy { get; set; }

        public Expression<Func<TEntity,object>> OrderByDescending { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }
    }
}
