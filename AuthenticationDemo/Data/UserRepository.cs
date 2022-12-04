using AuthenticationDemo.Interfaces;
using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemo.Data;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }
    public async Task<IdentityResult> DeleteUser(User user)
    {
        return await _userManager.DeleteAsync(user);
    }
    public async Task<IdentityResult> Update(User user)
    {
        return await _userManager.UpdateAsync(user);
    }
    public Task<User> GetUserByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }
    public Task<User> GetUserByIdAsync(string id)
    {
        return _userManager.FindByIdAsync(id);
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userManager.Users.ToListAsync();

    }
    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;
        return await _userManager.CheckPasswordAsync(user, password);
    }
}
