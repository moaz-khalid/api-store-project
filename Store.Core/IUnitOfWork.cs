using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        //create a generic repository for each entity
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;


    }
}
