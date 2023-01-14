using AuthenticationDemo.Enums;
using AuthenticationDemo.Interfaces;

namespace AuthenticationDemo.Models;

public class Response<T> //: IResponse<T> where T : class
{
    public Response(ResponseStatus status, string? message, T data, int count = 0)
    {
        Status = status;
        Message = message;
        Count = count;
        Data = data;
    }
    public Response(ResponseStatus status, string? message)
    {
        Status = status;
        Message = message;
    }
    public Response() { }


    public ResponseStatus Status { get; set; }
    public string? Message { get; set; }
    public int Count { get; set; }
    public T? Data { get; set; }
}
