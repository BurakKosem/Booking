using System;
using System.Net;

namespace SharedService.Result;

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public string? ErrorMessage { get; set; }
    public List<string> Errors { get; set; } = new();

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    protected Result() { }

    public static Result Success()
        => new Result { IsSuccess = true, StatusCode = HttpStatusCode.OK };
    public static Result Success(HttpStatusCode httpStatusCode)
        => new Result { IsSuccess = true, StatusCode = httpStatusCode };

    public static Result Failure(string errorMessage)
        => new Result
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            StatusCode = HttpStatusCode.BadRequest
        };
    public static Result Failure(string errorMessage, HttpStatusCode httpStatusCode)
        => new Result { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = httpStatusCode };

    public static Result Failure(List<string> errors)
        => new Result
        {
            IsSuccess = false,
            Errors = errors,
            ErrorMessage = string.Join("; ", errors),
            StatusCode = HttpStatusCode.BadRequest
        };

    public static Result Failure(List<string> errors, HttpStatusCode statusCode)
        => new Result
        {
            IsSuccess = false,
            Errors = errors,
            ErrorMessage = string.Join("; ", errors),
            StatusCode = statusCode
        };

}

public class Result<T> : Result
{
    protected Result() : base() { }
    public T? Data { get; set; }

    public static Result<T> Success(T data)
        => new Result<T> { IsSuccess = true, Data = data, StatusCode = HttpStatusCode.OK };
    public static Result<T> Success(T data, HttpStatusCode httpStatusCode)
        => new Result<T> { IsSuccess = true, Data = data, StatusCode = httpStatusCode };

    public new static Result<T> Failure(string errorMessage)
        => new Result<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            StatusCode = HttpStatusCode.BadRequest
        };

    public new static Result<T> Failure(string errorMessage, HttpStatusCode statusCode)
            => new Result<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
    public new static Result<T> Failure(List<string> errors)
        => new Result<T>
        {
            IsSuccess = false,
            Errors = errors,
            ErrorMessage = string.Join("; ", errors),
            StatusCode = HttpStatusCode.BadRequest
        };
    public new static Result<T> Failure(List<string> errors, HttpStatusCode statusCode)
        => new Result<T>
        {
            IsSuccess = false,
            Errors = errors,
            ErrorMessage = string.Join("; ", errors),
            StatusCode = statusCode
        };
}
