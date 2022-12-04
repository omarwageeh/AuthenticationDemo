using AuthenticationDemo.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationDemo.Interfaces;

public interface ITokenProvider
{
    public string GetToken(User user);
}
