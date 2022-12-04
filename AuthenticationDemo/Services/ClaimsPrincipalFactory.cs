using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AuthenticationDemo.Services;

public class ClaimsPrincipalFactory
{
    public class CustomClaimsPrincipalFactory :
         UserClaimsPrincipalFactory<User>
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger _logger;
        public CustomClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor, IHttpContextAccessor httpContext, ILogger<CustomClaimsPrincipalFactory> logger)
                : base(userManager, optionsAccessor)
        {
            _httpContext = httpContext;
            _logger = logger;
        }

        // This method is called only when login. It means that "the drawback 
        // of calling the database with each HTTP request" never happen.
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            if (user.Id != null)
            {
                _logger.LogInformation(principal.ToString());
                if (principal.Identity != null)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaims(
                        new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) });
                }
            }
            _httpContext.HttpContext.User = principal;
            return principal;
        }
    }
}
