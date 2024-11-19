using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITaskRepository : IBaseRepository<Tasks>
    {
        Task<List<Tasks>> GetAllByUserId(int userId, int? sortBy);
        Task<List<Tasks>> GetAllFilterByPriority(int? sortBy, string? searchTerm);
        Task<List<Tasks>> GetTasksByFirstLetterSorted(int userId);
        Task<List<Tasks>> GetAllFilterByStartDate(int userId);
        Task<List<Tasks>> GetAllFilterByStatus(int userId); 
    }
}
