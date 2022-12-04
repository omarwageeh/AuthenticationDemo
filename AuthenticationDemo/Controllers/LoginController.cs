using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationDemo.Enums;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data;

namespace AuthenticationDemo.Controllers;

//[AllowAnonymous]
//[ApiController]
//[Route("Login")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _config;
    private readonly UserManager<Models.User> _userManager;

    public LoginController(ILogger<LoginController> logger, IConfiguration config, UserManager<Models.User> userManager)
    {
        _logger = logger;
        _config = config;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<Response> Login([FromBody] LoginCommand model)
    {
        Response response = new Response();
        if (ModelState.IsValid)
        {
            response.Status = ResponseStatus.Ok;
            response.Message = "Model is Valid";
        }
        
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user!, model.Password))
        {
            response.Message = "User Authorized";
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = GetToken(authClaims);

            response.Data = new Dictionary<string, object>
        {
            {"Token", new JwtSecurityTokenHandler().WriteToken(token) }

        };
        }
        else
        {
            response.Message = "Incorrect Credentials";
        }


        return response;
    }

   
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
