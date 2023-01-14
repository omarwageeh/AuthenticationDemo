using AuthenticationDemo.DTOS;
using AuthenticationDemo.Enums;
using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo.Controllers;
[ApiController]
[Route("api/[Controller]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly ILogger<AccountController> _logger;
    public AccountController(AccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<Response<IdentityResult>>> Register([FromBody] UserRegister model)
    {
        try
        {
            var ret = new Response<IdentityResult>();
            var res = await _accountService.RegisterUserAsync(model);
            if (res.Succeeded)
                return Ok(new Response<IdentityResult>(ResponseStatus.Ok, "Success", res));
            return BadRequest(res);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<Response<Token>>> Login([FromBody] UserLogin model)
    {
        try
        {

            var passCheck = await _accountService.LoginUserAsync(model.Email, model.Password);
            if (passCheck)
            {
                var token = await _accountService.GetTokenAsync(model.Email);

                return Ok(new Response<Token>(ResponseStatus.Ok, "User Validated", token, 1));
            }
            return BadRequest(new List<object>() { model, "Invalid Credentials" });
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }

    [HttpGet("GetUsers")]
    public async Task<ActionResult<Response<IEnumerable<UserDto>>>> GetUsers()
    {
        try
        {
            var res = await _accountService.GetUsersAsync();
            if(res==null)
                return BadRequest(new Response<UserDto>(ResponseStatus.Ok, "Failed To Get Users"));
            var ret = new Response<IEnumerable<UserDto>>(ResponseStatus.Ok, "Success", res, res.Count());
            return Ok(ret);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }
}
