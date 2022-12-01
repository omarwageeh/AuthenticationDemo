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

[AllowAnonymous]
[ApiController]
[Route("Login")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _config;
    private readonly UserManager<Models.User> _userManager;
    private readonly SignInManager<Models.User> _signInManager;

    public LoginController(ILogger<LoginController> logger, IConfiguration config, UserManager<Models.User> userManager, SignInManager<User> signInManager)
    {
        _logger = logger;
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
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
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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

    [HttpPost("LoginSignInManager")]
    public async Task<Response> LoginTrySignInManager([FromBody] LoginCommand model)
    {
        Response response = new Response();
        if (ModelState.IsValid)
        {
            response.Status = ResponseStatus.Ok;
            response.Message = "Model is Valid";
        }
        var user = await _userManager.FindByEmailAsync(model.Email);

        var userSignIn = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        if (userSignIn.Succeeded)
        {
            var prin = User.Identities.AsEnumerable();
            var cl = new ClaimsPrincipal(prin);
            bool isSignedIn =  _signInManager.IsSignedIn(cl);
            //var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            //vaawait _signInManager.SignInAsync(user, true, principal.ToString());
            var token = await _userManager.GenerateUserTokenAsync(user, "Microsoft", "Login");
           // bool authorized = await _userManager.VerifyUserTokenAsync(user, "Miccrosoft", "Login", token);
            await _userManager.SetAuthenticationTokenAsync(user, "Microsoft", "RefreshToken", token);
            response.Data = token;
        }
        //var status = await _userManager.S
        //_logger.LogInformation(status.ToString());
        
        return response;
    }
    //use Access_Token
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
