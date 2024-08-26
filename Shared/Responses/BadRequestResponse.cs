namespace Pharmacy.Shared.Responses;



public class BadRequestResponse : BaseResponse
{
    public string Message;
    public BadRequestResponse(string message) : base(400) =>
        Message = message;
}