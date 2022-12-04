using AuthenticationDemo.Enums;
using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Org.BouncyCastle.Math.EC;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers;
[ApiController]
[Route("api/[Controller]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<AccountController> _logger;
    public AccountController(AccountService accountService, ILogger<AccountController> logger,ITokenProvider tokenProvider)
    {
        _accountService = accountService;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegisterCommand model)
    {
        try
        {
            var res = await _accountService.RegisterUserAsync(model);
            if (res.Succeeded)
                return Ok(res);
            return BadRequest(res);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ITokenProvider>> Login([FromBody] LoginCommand model)
    {
        try
        {

            var passCheck = await _accountService.LoginUserAsync(model.Email, model.Password);
            if (passCheck)
            {
                var token = await _accountService.GetTokenAsync(model.Email);

                return Ok(token);
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
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var ret = await _accountService.GetUsersAsync();
            return Ok(new Dictionary<string, object>{ { "count", ret.Count() }, { "users", ret } } );
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }
}
