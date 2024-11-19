using BusinessObject.Models;
using BusinessObject.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAccountRepository : IBaseRepository<ApplicationUsers>
    {
        Task<ApplicationUsers> GetByEmail(string email);

        Task<ApplicationUsers> GetByIdAsync(int id);
        Task<List<ApplicationUsers>> GetAllAsync();
    }
}
