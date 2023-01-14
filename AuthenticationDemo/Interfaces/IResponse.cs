using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemo.Interfaces;

public interface IResponse<T>
{
    public IResponse<T> Ok(string message);
    public IResponse<T> Ok(T Data, string message = "", int count = 0);
    public IResponse<T> NotFound(string message);
    public IResponse<T> NotFound(string message, T Data, int count = 0 );

}
