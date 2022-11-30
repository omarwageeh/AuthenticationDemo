using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AuthenticationDemo.Models;

public class RegisterCommand
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
    public string? FacebookID { get; set; }
    public string? GoogleID { get; set; }
}
