using BusinessObject.Models;
using BusinessObject.RequestModel;
using Repository.Interface;
using Service.Interface;

namespace Service
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IAccountRepository _accountRepository;

        public TaskService(ITaskRepository taskRepository, IAccountRepository accountRepository)
        {
            _taskRepository = taskRepository;
            _accountRepository = accountRepository;
        }

        public async Task Add(int UserId, TasksRequest request)
        {
            var account = await _accountRepository.GetById(UserId);
            if (account != null)
            {
                Tasks tasks = new Tasks
                {
                    Name = request.Name,
                    StartTime = request.StartTime.ToUniversalTime(),
                    EndTime = request.EndTime.ToUniversalTime(),
                    Description = request.Description,
                    Priority = 3,
                    Status = 0,
                    Flaged = false
                };
                if (request.Priority != 0)
                {
                    tasks.Priority = request.Priority;
                }
                if (request.Status != 0)
                {
                    tasks.Status = request.Status;
                }
                if (request.Flaged)
                {
                    tasks.Flaged = true;
                }
                account.Tasks.Add(tasks);
                await _accountRepository.Update(account);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task Delete(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task != null)
            {
                await _taskRepository.Delete(task);
            }
            else
            {
                throw new Exception("Task not found");
            }
        }

        public async Task<List<Tasks>> GetAll()
        {
            return await _taskRepository.GetAll();
        }

        public async Task<List<Tasks>> GetAllAsync(int? sortBy, string? searchTerm)
        {
            return await _taskRepository.GetAllFilterByPriority(sortBy, searchTerm);
        }

        public async Task<List<Tasks>> GetAllByUserId(int userId, int? sortBy)
        {
            return await _taskRepository.GetAllByUserId(userId, sortBy);
        }

        public async Task<List<Tasks>> GetAllFilterByStartDate(int userId)
        {
            return await _taskRepository.GetAllFilterByStartDate(userId);
        }

        public async Task<List<Tasks>> GetAllFilterByStatus(int userId)
        {
            return await _taskRepository.GetAllFilterByStatus(userId);
        }

        public async Task<Tasks> GetById(int id)
        {
            return await _taskRepository.GetById(id);
        }

        public async Task<List<Tasks>> GetTasksByFirstLetterSorted(int userId)
        {
            return await _taskRepository.GetTasksByFirstLetterSorted(userId);
        }

        public async Task Update(int id, TasksRequest request)
        {
            var task = await _taskRepository.GetById(id);
            if (task != null)
            {
                task.Priority = request.Priority;
                task.Status = request.Status;
                task.Flaged = request.Flaged;
                task.StartTime = request.StartTime.ToUniversalTime();
                task.EndTime = request.EndTime.ToUniversalTime();
                task.Description = request.Description;
                task.Name = request.Name;

                await _taskRepository.Update(task);
            }
            else
            {
                throw new Exception("Task not found");
            }
        }
    }
}
