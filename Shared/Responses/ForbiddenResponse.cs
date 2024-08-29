namespace Pharmacy.Shared.Responses;



public class ForbiddenResponse : BaseResponse
{
    public string Message => "Permission denied";
    public ForbiddenResponse() : base(403) {}
}
