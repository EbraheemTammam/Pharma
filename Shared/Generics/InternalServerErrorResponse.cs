namespace Pharmacy.Shared.Generics;


public class InternalServerErrorResponse : BaseResponse
{
    public string Message {get; set;}
    public InternalServerErrorResponse(string message = "Something went wrong") : base(500) =>
        Message = message;
}
