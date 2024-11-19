using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DBContext _dbContext;

        public BaseRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected DbSet<T> _dbSet
        {
            get
            {
                var dbSet = GetDbSet<T>();
                return dbSet;
            }
        }

        protected DbSet<T> GetDbSet<T>() where T : BaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            return dbSet;
        }

        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
