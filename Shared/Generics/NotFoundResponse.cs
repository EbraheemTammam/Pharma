namespace Pharmacy.Shared.Generics;



public class NotFoundResponse : BaseResponse
{
    public string Message;
    public NotFoundResponse(object id, string resource) : base(false, 404) =>
        Message = $"{resource} with id {id} Not Found";
}
