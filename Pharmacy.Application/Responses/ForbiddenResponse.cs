namespace Pharmacy.Application.Responses;



public class ForbiddenResponse : BaseResponse
{
    public string Message => "Permission denied";
    public ForbiddenResponse() : base(403) {}
}
