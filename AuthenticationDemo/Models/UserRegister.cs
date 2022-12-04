using AuthenticationDemo.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Models;

public class UserRegister
{
    [Required]
    [RegularExpression(@"^([\u0621-\u064A\s]+|[a-zA-Z\s]+)$")]
    public string FirstName { get; set; }
    [Required]
    [RegularExpression(@"^([\u0621-\u064A\s]+|[a-zA-Z\s]+)$")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string? FacebookId { get; set; }
    public string? GoogleId { get; set; }
    public Language LanguageId { get; set; } = 0;
}
