using BusinessObject.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace TooDooApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? sortBy, string? searchTerm = null)
        {
            try
            {
                var tasks = await _taskService.GetAllAsync(sortBy, searchTerm);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("priority/userId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId, int? sortBy = 1)
        {
            try
            {
                var tasks = await _taskService.GetAllByUserId(userId, sortBy);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("letter/userId/{userId}")]
        public async Task<IActionResult> GetTasksByFirstLetterSorted(int userId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByFirstLetterSorted(userId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("status/userId/{userId}")]
        public async Task<IActionResult> GetTaskByStatus(int userId)
        {
            try
            {
                var tasks = await _taskService.GetAllFilterByStatus(userId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("start-date/userId/{userId}")]
        public async Task<IActionResult> GetTaskByStartDate(int userId)
        {
            try
            {
                var tasks = await _taskService.GetAllFilterByStartDate(userId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var task = await _taskService.GetById(id);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(int UserId, [FromBody] TasksRequest request)
        {
            try
            {
                await _taskService.Add(UserId, request);
                return Ok("Created successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] TasksRequest request)
        {
            try
            {
                await _taskService.Update(id, request);
                return Ok("Updated successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.Delete(id);
                return Ok("Deleted successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
