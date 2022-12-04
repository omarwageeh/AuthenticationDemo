using AuthenticationDemo.Enums;
using AuthenticationDemo.Models;

namespace AuthenticationDemo.DTOS;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
    public Language LanguageId { get; set; }
    public List<Style> Styles { get; set; } = new List<Style>();
    public List<Photo> Photos { get; set; } = new();
}
