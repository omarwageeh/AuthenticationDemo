using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo.Controllers;

[Route("SetPreferredStyles")]
[ApiController]
[AllowAnonymous]
public class PrefStylesController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }
    public PrefStylesController(UserManager<User> userManager)
    {
        this._userManager = userManager;
    }
    [HttpPost]
    public async Task<Response> SetPreferredStyles(List<int> styles)
    {
        User user = await _userManager.FindByEmailAsync("user@example.com"); 
        Response response = new Response();
        response.Status = ResponseStatus.Ok;
        response.Message = "Preferred Styles Set";
        response.Data = new Dictionary<string, object>()
        {
            { "Success", true},
            {"List", styles},
            {"UserToken", await _userManager.FindByEmailAsync("user@example.com") }
        };
        return response;
    }

}
