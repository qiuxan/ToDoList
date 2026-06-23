namespace ToDoList.Application.Common;

public enum ResultStatus
{
    Success,
    NotFound,
    ValidationError
}

public sealed record Result(
    ResultStatus Status,
    string[] Errors)
{
    public bool IsSuccess => Status == ResultStatus.Success;

    public static Result Success()
    {
        return new Result(ResultStatus.Success, []);
    }

    public static Result NotFound()
    {
        return new Result(ResultStatus.NotFound, []);
    }

    public static Result ValidationError(params string[] errors)
    {
        return new Result(ResultStatus.ValidationError, errors);
    }
}

public sealed record Result<T>(
    ResultStatus Status,
    T? Value,
    string[] Errors)
{
    public bool IsSuccess => Status == ResultStatus.Success;

    public static Result<T> Success(T value)
    {
        return new Result<T>(ResultStatus.Success, value, []);
    }

    public static Result<T> NotFound()
    {
        return new Result<T>(ResultStatus.NotFound, default, []);
    }

    public static Result<T> ValidationError(params string[] errors)
    {
        return new Result<T>(ResultStatus.ValidationError, default, errors);
    }
}