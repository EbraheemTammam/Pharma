namespace Pharmacy.Shared.Responses;



public class UnAuthorizedResponse : BaseResponse
{
    public string Message => "Email or password is incorrect";
    public UnAuthorizedResponse() : base(401) {}
}
