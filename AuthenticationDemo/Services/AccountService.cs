using AuthenticationDemo.DTOS;
using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace AuthenticationDemo.Services;

public class AccountService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenProvider _tokenService;
    private readonly IMapper _mapper;
    public AccountService(IUserRepository userRepo, ITokenProvider tokenService, IMapper mapper)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    public async Task<IdentityResult> RegisterUserAsync(UserRegister model)
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
    public async Task<UserDto> GetUserAsync(string id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);
        var userToReturn = _mapper.Map<UserDto>(user);
        return userToReturn;
    }
    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _userRepo.GetUsersAsync();
        var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(users);
        return usersToReturn;
    }
    public async Task<Token> GetTokenAsync(string email, bool refreshToken = false)
    {
        var user = await _userRepo.GetUserByEmailAsync(email);
        var token = new Token();
        
        token.AccessToken = _tokenService.GetToken(user);
        return token;

    }
}
