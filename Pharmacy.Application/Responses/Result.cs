namespace Pharmacy.Application.Responses;

public record Result
{
    public bool Succeeded { get; }
    public Response Response {  get; }
    protected Result(bool succeeded, Response response)
    {
        if ((succeeded && response.StatusCode > 300 )||
            (!succeeded && response.StatusCode < 300))
            throw new InvalidOperationException();
        Succeeded = succeeded;
        Response = response;
    }
    public static Result Success(int statusCode = 200) => new(true, Response.Valid(statusCode));
    public static Result Fail(Response response) => new(false, response);
    public static Result<TResult> Success<TResult>(TResult data, int statusCode = 200) => new Result<TResult>(data, statusCode);
    public static Result<TResult> Fail<TResult>(Response response) => new Result<TResult>(response);
}

public record Result<TResult> : Result
{
    public TResult? Data { get; }
    public Result(Response error) : base(false, error)
    {

    }
    public Result(TResult data, int statusCode = 200) : base(true, Response.Valid(statusCode))
    {
        Data = data;
    }

}
