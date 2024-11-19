using BusinessObject.Models;
using Repository.Interface;

namespace Repository
{
    public class SubcriptionRepository : BaseRepository<Subcriptions>, ISubcriptionRepository
    {
        private readonly DBContext _dbContext;

        public SubcriptionRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(int id)
        {
            var subcription = await GetById(id);
            if (subcription != null)
            {
                _dbContext.Subcriptions.Remove(subcription);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
