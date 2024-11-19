using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISubcriptionRepository : IBaseRepository<Subcriptions>
    {
        Task DeleteAsync(int id);
    }
}
