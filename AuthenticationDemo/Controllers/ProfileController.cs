using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers;
[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly ProfileService _profileService;
    public ProfileController(ILogger<ProfileController> logger, ProfileService profileService)
    {
        _logger = logger;
        _profileService = profileService;
    }

    [HttpGet("GetProfile")]
    [Authorize]
    public async Task<ActionResult<User>> GetProfile()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _profileService.GetProfileById(userId);
            return Ok(user);
        }
        catch(Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }

    }

    [HttpPost("SetStyles")]
    [Authorize]
    public async Task<ActionResult<bool>> SetStyles(List<int> styles)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var res = await _profileService.SetStyles(styles, userId); //should set styles here
            if(res)
                return Ok(await _profileService.GetProfileById(userId)); 
            return BadRequest("Failed to SetStyles");
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e);
        }
    }
}
