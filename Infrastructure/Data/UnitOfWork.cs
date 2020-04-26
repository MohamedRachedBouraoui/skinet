using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityType = typeof(TEntity);
            var entityName = entityType.Name;

            if (_repositories.ContainsKey(entityName) == false)
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(entityType), _storeContext);

                // Add an instance of the repo to the hashtable
                _repositories.Add(entityName, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[entityName];
        }

        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _storeContext.Dispose();
        }
    }
}