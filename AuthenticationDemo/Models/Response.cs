using AuthenticationDemo.Enums;

namespace AuthenticationDemo.Models;

public class Response
{
    public ResponseStatus Status { get; set; }
    public string? Message { get; set; }
    public int Count { get; set; }
    public object? Data { get; set; }
}
