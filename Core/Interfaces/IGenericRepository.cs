using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);


        // Theses methods don't have to be Async because they will
        // only act on the dbContext and NOT into the DB
        // it's the 'Complete()' method on the UOW witch will do it.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}