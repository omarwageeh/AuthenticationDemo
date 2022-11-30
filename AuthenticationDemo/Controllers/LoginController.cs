using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using AuthenticationDemo.Enums;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationDemo.Controllers;
[Route("Login")]

public class LoginController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _iConfig;
    public LoginController(UserManager<User> userManager, IConfiguration iConfig)
    {
        _userManager = userManager;
        _iConfig = iConfig;
    }
    [HttpPost]
    [AllowAnonymous]
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
                new Claim("Email", user.Email),
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
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfig["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _iConfig["Jwt:Issuer"],
            audience: _iConfig["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
