using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers;
[Authorize]
[ApiController]
[Route("EditUserProfile")]
public class EditProfileController : Controller
{
    private readonly ILogger<EditProfileController> _logger;
    private readonly UserManager<User> _userManager;
    public EditProfileController(ILogger<EditProfileController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<Response> EditUserProfile(EditProfileCommand model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        if(model.ProfilePicture != null)
        {
            user.ProfilePicture = model.ProfilePicture;
        }
        var result = await _userManager.UpdateAsync(user);
        Response response = new Response();
        response.Status = Enums.ResponseStatus.Ok;
        response.Message = "Changes Saved";
        response.Data = true;
        return response;
    }
}
