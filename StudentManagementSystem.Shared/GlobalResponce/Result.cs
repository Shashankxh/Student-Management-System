namespace StudentManagementSystem.Shared.GlobalResponce;

public class Result<T> where T : class
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public T? Data { get; set; } = null;

    public Result(bool isSuccess, int statuscode, T? data)
    {
        IsSuccess = isSuccess;
        StatusCode = statuscode;
        Data = data;
    }
    public Result(bool isSuccess, int statuscode, string error)
    {
        IsSuccess = isSuccess;
        StatusCode = statuscode;
        Error = error;
    }

    public static Result<T> Success(T? data) => new(true, 200, data);
    public static Result<T> Failure(string error) => new(false, 400, error);

}

