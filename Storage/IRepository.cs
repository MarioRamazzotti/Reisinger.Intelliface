using Microsoft.EntityFrameworkCore;
using Reisinger_Intelliface_1_0.Domain;
using Reisinger_Intelliface_1_0.Data;
using System.Linq.Expressions;

namespace Reisinger_Intelliface_1_0.Storage
{

    public interface IRepository<T> where T : class, IStorableObject
    {


        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task AddOrUpdateAsync(T entity);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(Guid entity);

        /// <summary>
        /// Returns all elements in the assigned database table
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Returns an object by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T ById(Guid id);

        /// <summary>
        /// Returns an integer that represents the amount of stored elements in the repository
        /// </summary>
        /// <returns></returns>
        int Count();

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes);


        /// <summary>
        /// Adds an object to the database
        /// </summary>
        /// <param name="objectToStore"></param>
        void Add(T objectToStore);

        /// <summary>
        /// Updates an object in the database
        /// </summary>
        /// <param name="objectToStore"></param>
        void Update(T objectToStore);

        void AddOrUpdate(T entity);

        /// <summary>
        /// Returns a queryable object for specific purpose
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();
        /// <summary>
        /// Removes an object from the database
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);

        /// <summary>
        /// Indicates if an object with the given id is existing in the database table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(Guid id);

    }
}
