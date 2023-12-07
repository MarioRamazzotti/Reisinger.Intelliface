using Microsoft.EntityFrameworkCore;
using Reisinger_Intelliface_1_0.Data;
using Reisinger_Intelliface_1_0.Domain;
using System.Linq.Expressions;

namespace Reisinger_Intelliface_1_0.Storage
{
    public class BasicRepository<T> : IRepository<T>, IDisposable where T : class, IStorableObject
    {
        public readonly IntellifaceDataContext _context;

        public BasicRepository(IntellifaceDataContext context)
        {
            _context = context;
        }


        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationProperty)
        {
            return _context.Set<T>().Include(navigationProperty);
        }

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(e => e.ID == id);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            if (Exists(entity.ID))
            {
                await UpdateAsync(entity);

            }
            else
            {
                await AddAsync(entity);
            }
        }


        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);



            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid entity)
        {
            _context.Set<T>().Remove(ById(entity));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns all elements in the assigned database table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return GetAllAsync().Result;
        }
        /// <summary>
        /// Returns an object by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T ById(Guid id)
        {
            return GetByIdAsync(id).Result;
        }

        /// <summary>
        /// Returns an integer that represents the amount of stored elements in the repository
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            System.Diagnostics.Debug.WriteLine($"Find-Methode aufgerufen mit Prädikat: {predicate}");
            return _context.Set<T>().Where(predicate);
        }


        /// <summary>
        /// Adds an object to the database
        /// </summary>
        /// <param name="objectToStore"></param>
        public void Add(T objectToStore)
        {
            AddAsync(objectToStore).Wait();
        }

        /// <summary>
        /// Updates an object in the database
        /// </summary>
        /// <param name="objectToStore"></param>
        public void Update(T objectToStore)
        {
            UpdateAsync(objectToStore).Wait();
        }

        public void AddOrUpdate(T entity)
        {
            if (Exists(entity.ID))
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
        }

        /// <summary>
        /// Returns a queryable object for specific purpose
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Removes an object from the database
        /// </summary>
        /// <param name="id"></param>
        public void Remove(Guid id)
        {
            DeleteAsync(id).Wait();
        }

        /// <summary>
        /// Indicates if an object with the given id is existing in the database table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(Guid id)
        {
            return GetQueryable().Any(m => m.ID == id);
        }


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            //this.context = null;
            //this.dbSet = null;
        }
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

    }
}
