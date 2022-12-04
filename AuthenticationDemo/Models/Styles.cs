using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationDemo.Models;

[Table("Style")]
public class Style
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public List<User> Users { get; set; }
}