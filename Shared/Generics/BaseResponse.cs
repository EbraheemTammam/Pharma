namespace Pharmacy.Shared.Generics;


public abstract class BaseResponse
{
    public bool Success {get; set;}
    public int StatusCode {get;}
    protected BaseResponse(bool success, int statusCode)
    {
        Success = success;
        StatusCode = statusCode;
    }
}
