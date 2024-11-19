using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository
{
    public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
    {
        private readonly DBContext _dbContext;

        public TaskRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tasks>> GetAllFilterByPriority(int? sortBy, string? searchTerm)
        {
            var query = _dbSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(t => t.Name.Contains(searchTerm));
            }
            if (sortBy.HasValue)
            {
                query = query.OrderByDescending(t => t.Priority == sortBy.Value);
            }
            var tasks = await query.ToListAsync();
            return tasks;
        }

        public async Task<List<Tasks>> GetAllByUserId(int userId, int? sortBy)
        {
            var query = _dbContext.Users
                        .Where(user => user.Id == userId)
                        .SelectMany(user => user.Tasks);
            if (sortBy.HasValue)
            {
                query = query.OrderByDescending(t => t.Priority == sortBy.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Tasks>> GetTasksByFirstLetterSorted(int userId)
        {
            string defaultLetter = "a";

            var tasks = await _dbContext.Users
                        .Where(user => user.Id == userId)
                        .SelectMany(user => user.Tasks)
                        .OrderBy(t => t.Name)
                        .ToListAsync();

            return tasks;
        }

        public async Task<List<Tasks>> GetAllFilterByStartDate(int userId)
        {
            var tasks = await _dbContext.Users
                        .Where(user => user.Id == userId)
                        .SelectMany(user => user.Tasks)
                        .OrderBy(t => t.StartTime)
                        .ToListAsync();

            return tasks;
        }

        public async Task<List<Tasks>> GetAllFilterByStatus(int userId)
        {
            var tasks = await _dbContext.Users
                       .Where(user => user.Id == userId)
                       .SelectMany(user => user.Tasks)
                       .OrderBy(t => t.Status)
                       .ToListAsync();

            return tasks;
        }
    }
}
