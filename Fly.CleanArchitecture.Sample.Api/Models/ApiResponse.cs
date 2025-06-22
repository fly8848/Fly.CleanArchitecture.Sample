namespace Fly.CleanArchitecture.Sample.Api.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }

    public static ApiResponse<T> Success(T? data)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            Message = message
        };
    }
}