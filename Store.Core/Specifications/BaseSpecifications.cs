using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Store.Core.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get ; set ; } = null; // Filter condition (where clause)
        public List<Expression<Func<TEntity, object>>> Include { get ; set ; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderByDescending { get; set ; } = null;
        public Expression<Func<TEntity, object>> OrderBy { get ; set; } = null;

        public int Skip { get ; set ; }

        public int Take { get ; set ; }


        public bool IsPaginationEnabled { get ; set ; }


        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;

        }

        public BaseSpecifications()
        {

        }

        public void AddOrderBy(Expression<Func<TEntity, object>> Expression)
        {
            OrderBy = Expression;
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>> Expression)
        {
            OrderByDescending = Expression;
        }

        public void ApplyPagination(int skip, int take)
        {

            IsPaginationEnabled = true;

            Skip = skip;
            Take = take;

        }

    }
}
