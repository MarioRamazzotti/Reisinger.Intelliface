using Microsoft.EntityFrameworkCore;
using Reisingher_Intelliface_1_0.Domain;
using System.Linq.Expressions;

namespace Reisingher_Intelliface_1_0.Storage
{
    public class MockRepository<T> : IRepository<T> where T : class, IStorableObject
    {

        private Dictionary<Guid, T> _repository = new Dictionary<Guid, T>();


        public void Add(T objectToStore)
        {
            _repository.Add(objectToStore.ID, objectToStore);
        }

        public async Task AddAsync(T entity)
        {
            _repository.Add(entity.ID, entity);

        }

        public void AddOrUpdate(T entity)
        {
            AddOrUpdateAsync(entity).Wait();
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

        public T ById(Guid id)
        {
            if (_repository.ContainsKey(id))
            {
                return _repository[id];
            }
            else
            {
                return null;
            }
        }

        public int Count()
        {
            return _repository.Count;
        }

        public async Task DeleteAsync(Guid entity)
        {
            _repository.Remove(entity);

            await Task.CompletedTask;
        }

        public bool Exists(Guid id)
        {
            return _repository.ContainsKey(id);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return _repository.Values.ToList();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            return ById(id);

        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return ById(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _repository.Values.AsQueryable();
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public void Update(T objectToStore)
        {
            UpdateAsync(objectToStore).Wait();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository[entity.ID] = entity;

        }
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes) => IQueryable<T>;
    }
}
