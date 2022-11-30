using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AuthenticationDemo.Controllers;

[Route("SetPreferredStyles")]
[ApiController]
[AllowAnonymous]
public class PrefStylesController : ControllerBase
{
    private readonly ILogger<PrefStylesController> _logger;
    private readonly UserManager<User> _userManager;
    public PrefStylesController(ILogger<PrefStylesController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }


    [HttpPost]
    [Authorize]
    public async Task<Response> SetPreferredStyles(List<int> styles)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        string email = "";
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            // or
            email = identity.FindFirst("Email")!.Value;

        }
    
        Response response = new Response();
        response.Status = ResponseStatus.Ok;
        response.Message = "Preferred Styles Set";
        response.Data = new Dictionary<string, object>()
        {
            { "Success", true},
            {"List", styles},
            {"User", await _userManager.FindByEmailAsync(email) }
        };
        return response;
    }

}
