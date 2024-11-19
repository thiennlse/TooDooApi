using BusinessObject.RequestModel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Validate;

namespace TooDooApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly AccountValidate _accountValidate;

        public AccountController(IAccountService accountService, AccountValidate accountValidate)
        {
            _accountService = accountService;
            _accountValidate = accountValidate;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var account = await _accountService.GetAll();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AccountRequest request)
        {
            try
            {
                ValidationResult result = _accountValidate.Validate(request);
                if (result.IsValid)
                {
                    var account = await _accountService.Login(request.Email, request.Password);
                    if (account != null)
                    {
                        return Ok(account);
                    }
                    return NotFound("Wrong account or password");
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var account = await _accountService.GetById(id);
                if (account == null)
                {
                    return BadRequest("Not found");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AccountRequest request)
        {
            try
            {
                ValidationResult result = _accountValidate.Validate(request);
                if (result.IsValid)
                {
                    await _accountService.Register(request);
                    return Ok("Registered successful");
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

        [HttpPost]
        [Route("subcription")]
        public async Task<IActionResult> SubcriptionForUser(int userId, int subcriptionId)
        {
            try
            {
                await _accountService.SubcriptionRegister(userId, subcriptionId);
                return Ok("Register subcription successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
