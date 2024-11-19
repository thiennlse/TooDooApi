using BusinessObject.RequestModel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Validate;

namespace TooDooApi.Controllers
{
    [Route("api/subcriptions")]
    [ApiController]
    public class SubcriptionController : ControllerBase
    {
        private readonly ISubcriptionService _subcriptionService;
        private readonly SubcriptionValidate _subcriptionValidate;

        public SubcriptionController(ISubcriptionService subcriptionService, SubcriptionValidate subcriptionValidate)
        {
            _subcriptionService = subcriptionService;
            _subcriptionValidate = subcriptionValidate;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subcriptions = await _subcriptionService.GetAll();
                return Ok(subcriptions);
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
                var subcription = await _subcriptionService.GetById(id);
                return Ok(subcription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubcriptionRequest request)
        {
            try
            {
                ValidationResult result = _subcriptionValidate.Validate(request);
                if (result.IsValid)
                {
                    await _subcriptionService.Add(request);
                    return Ok("Created successful");
                }
                var errors = result.Errors.Select(e => (object)new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }).ToList();
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubcriptionRequest request)
        {
            try
            {
                ValidationResult result = _subcriptionValidate.Validate(request);
                if (result.IsValid)
                {
                    await _subcriptionService.Update(id, request);
                    return Ok("Updated successful");
                }
                var errors = result.Errors.Select(e => (object)new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }).ToList();
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subcription = await _subcriptionService.GetById(id);
                if (subcription != null)
                {
                    _subcriptionService.Delete(id);
                    return Ok("Deleted successful");
                }
                return BadRequest("Subcription not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
