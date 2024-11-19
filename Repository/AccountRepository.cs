using BusinessObject.Models;
using BusinessObject.RequestModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : BaseRepository<ApplicationUsers>, IAccountRepository
    {
        private readonly DBContext _dbContext;

        public AccountRepository(DBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ApplicationUsers>> GetAllAsync()
        {
            return await _dbSet
                .Include(m => m.UserSubcriptions
                .Where(subscription => subscription.EndDate > DateTime.Now.ToUniversalTime()))
                .ThenInclude(u => u.Subcriptions)
                .ToListAsync();
        }

        public async Task<ApplicationUsers> GetByEmail(string email)
        {
            return await _dbSet
                .Include(m => m.UserSubcriptions.Where(subscription => subscription.EndDate > DateTime.Now.ToUniversalTime()))
                .ThenInclude(u => u.Subcriptions)
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<ApplicationUsers> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(m => m.UserSubcriptions.Where(subscription => subscription.EndDate > DateTime.Now.ToUniversalTime()))
                .ThenInclude(u => u.Subcriptions)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
    }
}
