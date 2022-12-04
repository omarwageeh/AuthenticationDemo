using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationDemo.Enums;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationDemo.Controllers;

//[AllowAnonymous]
//[ApiController]
//[Route("RegisterUser")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly UserManager<Models.User> _userManager;

    public RegistrationController(ILogger<RegistrationController> logger, UserManager<Models.User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
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

        var user = new Models.User
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            FacebookId = model.FacebookId,
            GoogleId = model.GoogleId,
            UserName = model.FirstName + model.LastName,
            LanguageId = model.LanguageId,
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
