using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.Specifications;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _Context;

        public GenericRepository(StoreDbContext   Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>)await _Context.Products.OrderBy(p => p.Name).Include(p => p.Name).Include(p => p.Type).ToListAsync();
            }

                return await _Context.Set<TEntity>().ToListAsync();

        }
        public async Task<TEntity> GetAsync(TKey id)
        {

            if (typeof(TEntity) == typeof(Product))
            {
                return await _Context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int?)as TEntity;
            }


            return await _Context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _Context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
           _Context.Remove(entity);
        }



        public void Update(TEntity entity)
        {
           _Context.Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        { 
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity> GetWithSpecAsync(TKey id, ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_Context.Set<TEntity>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }
    }
}
