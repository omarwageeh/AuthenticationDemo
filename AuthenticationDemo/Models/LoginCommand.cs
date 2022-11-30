using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Models;

public class LoginCommand
{
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; }
    public string FacebookId { get; set; }
    public string GoogleId { get; set; }
}
