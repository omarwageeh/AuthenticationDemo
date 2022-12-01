using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers;

[Route("SetPreferredStyles")]
[ApiController]
[AllowAnonymous]
public class PrefStylesController : ControllerBase
{
    private readonly ILogger<PrefStylesController> _logger;
    private readonly UserManager<Models.User> _userManager;
    public PrefStylesController(ILogger<PrefStylesController> logger, UserManager<Models.User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }


    [HttpPost]
    [Authorize]
    public async Task<Response> SetPreferredStyles(List<int> styles)
    {
        var value = User.Identity.Name;
        
        var userId = User.FindFirstValue(ClaimTypes.Name);
        var user = User.Identity as ClaimsIdentity;
        //TODO Add funtionality for adding list of prefrences to database
        Response response = new Response();
        response.Status = ResponseStatus.Ok;
        response.Message = "Preferred Styles Set";
        response.Data = new Dictionary<string, object>()
        {
            { "Success", true},
            {"List", styles},
            {"User", await _userManager.FindByIdAsync(userId) }
        };
        return response;
    }

}
