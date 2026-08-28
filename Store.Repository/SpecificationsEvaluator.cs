using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Repository
{
    public static class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        //create and return a query based on the specifications provided
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec) 
        {
            var query = inputQuery;

            // Apply the filter criteria if it exists
            if (spec.Criteria != null) 
            {
                query = query.Where(spec.Criteria);
            }


            // Apply the ordering if it exists
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            // Apply pagination if enabled
            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Apply the include expressions if they exist
            query = spec.Include.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
