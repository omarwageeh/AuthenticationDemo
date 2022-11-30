using AuthenticationDemo.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationDemo.Models;

public class User : IdentityUser
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
    [NotMapped]
    public List<int> PreferredStyles { get; set; }
}
