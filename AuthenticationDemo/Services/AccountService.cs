using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;


namespace AuthenticationDemo.Services;

public class AccountService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenProvider _tokenService;
    public AccountService(IUserRepository userRepo, ITokenProvider tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }
    public async Task<IdentityResult> RegisterUserAsync(RegisterCommand model)
    {
        var user = new User
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            FacebookId = model.FacebookId,
            GoogleId = model.GoogleId,
            UserName = model.FirstName + model.LastName,
            LanguageId = model.LanguageId,
        };

        return await _userRepo.CreateAsync(user, model.Password);
    }
    public async Task<bool> LoginUserAsync(string email, string password)
    {
        return await _userRepo.CheckPasswordAsync(email, password);

    }
    public async Task<User> GetUserAsync(string id)
    {
       return await _userRepo.GetUserByIdAsync(id);
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepo.GetUsersAsync();
    }
    public async Task<Token> GetTokenAsync(string email, bool refreshToken = false)
    {
        var user = await _userRepo.GetUserByEmailAsync(email);
        var token = new Token();
        
        token.AccessToken = _tokenService.GetToken(user);
        return token;

    }
}
