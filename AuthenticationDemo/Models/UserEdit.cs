using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Models;

public class UserEdit
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? MineType { get; set; }
    public string? FileName { get; set; }
}
