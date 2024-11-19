using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepo
    {
        public OrderRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Orders>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Subcriptions)
                .ToListAsync();
        }
    }
}
