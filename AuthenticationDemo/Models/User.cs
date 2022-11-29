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
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    [Required]
    public string? FacebookID { get; set; }
    public string? GoogleID { get; set; }
    public string? ProfilePicture { get; set; }
    [NotMapped]
    public List<int> PreferredStyles { get; set; }
}
