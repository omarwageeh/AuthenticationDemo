using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers;
[Authorize]
[ApiController]
[Route("GetUserProfile")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly UserManager<User> _userManager;
    public ProfileController(ILogger<ProfileController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<Response> GetUserProfile()
    {
        var userClaims = HttpContext.User.Identity as ClaimsIdentity;
        string userId = userClaims.FindFirst("Id")!.Value;
        var user = await _userManager.FindByIdAsync(userId);
        Response response = new Response();
        response.Status = ResponseStatus.Ok;
        response.Message = "Found User Profile";
        response.Data = new Dictionary<string, object>
        {
            {"FirstName", user.FirstName },
            {"LastName", user.LastName },
            {"Email", user.Email },
            {"ProfilePicture", user.ProfilePicture }
        };
        return response;
    }
}
