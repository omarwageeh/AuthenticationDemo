using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationDemo.Enums;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationDemo.Controllers;
[AllowAnonymous]
[Route("RegisterUser")]

public class RegistrationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private IConfiguration _config;
    public RegistrationController(UserManager<User> userManager, IConfiguration config)
    {
        this._userManager = userManager;
        _config = config; 
    }
    [HttpPost]
    public async Task<Response> RegisterAsync([FromBody] RegisterCommand model)
    {
        Response response = new Response();
        if (ModelState.IsValid)
        {
            response.Status = ResponseStatus.Ok;
            response.Message = "model passed validation!!";
        }

        var user = new User
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            FacebookID = model.FacebookID,
            GoogleID = model.GoogleID,
            UserName = model.FirstName + model.LastName,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        
        response.Data = new Dictionary<string, object>()
        {
            {"Result", result},
            {"User", user },
        };

        return response;
    }
}
