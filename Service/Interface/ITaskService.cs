using BusinessObject.Models;
using BusinessObject.RequestModel;


namespace Service.Interface
{
    public interface ITaskService
    {
        Task<List<Tasks>> GetAll();
        Task<List<Tasks>> GetAllAsync(int? sortBy, string? searchTerm);
        Task<List<Tasks>> GetAllByUserId(int userId, int? sortBy);
        Task<List<Tasks>> GetTasksByFirstLetterSorted(int userId);
        Task<List<Tasks>> GetAllFilterByStartDate(int userId);
        Task<List<Tasks>> GetAllFilterByStatus(int userId);
        Task<Tasks> GetById(int id);
        Task Add(int UserId, TasksRequest request);
        Task Delete(int id);
        Task Update(int id, TasksRequest request);
    }
}
