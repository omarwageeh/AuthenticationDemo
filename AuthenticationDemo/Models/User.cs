using AuthenticationDemo.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationDemo.Models;

public class User : IdentityUser<int>
{
    [Required]
    [RegularExpression(@"^([\u0621-\u064A\s]+|[a-zA-Z\s]+)$")]
    public string FirstName { get; set; }
    [Required]
    [RegularExpression(@"^([\u0621-\u064A\s]+|[a-zA-Z\s]+)$")]
    public string LastName { get; set; }
    public bool IsEmailConfirmed { get; set; }
    [Required]
    public string? FacebookId { get; set; }
    public string? GoogleId { get; set; }
    public string? ProfilePicture { get; set; }
    public Language LanguageId { get; set; }
    public int StyleId { get; set; }
    public List<Style> Styles { get; set; } = new List<Style>();
    //public int PhotoId { get; set; }
    public List<Photo> Photos { get; set; } = new();
}
