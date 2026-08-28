using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;

        private readonly StoreDbContext _context;

        public UnitOfWork(StoreDbContext context) 
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;

            //check if the repository for the entity type already exists in the hashtable
            if (!_repositories.ContainsKey(type))
            {
                   var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type, repository);
            }

            return _repositories[type] as IGenericRepository<TEntity, TKey>;

        }
    }
}
