namespace Pharmacy.Application.Responses;


public abstract class BaseResponse
{
    public int StatusCode {get;}
    protected BaseResponse(int statusCode) =>
        StatusCode = statusCode;
}
