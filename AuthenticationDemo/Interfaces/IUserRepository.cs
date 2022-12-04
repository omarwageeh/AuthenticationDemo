using AuthenticationDemo.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationDemo.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> DeleteUser(User user);
    Task<IdentityResult> Update(User user);
    Task<User> GetUserByEmailAsync(string email);
    public Task<User> GetUserByIdAsync(string id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<bool> CheckPasswordAsync(string email, string password);
}
