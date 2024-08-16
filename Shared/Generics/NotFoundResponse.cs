namespace Pharmacy.Shared.Generics;



public class NotFoundResponse : BaseResponse
{
    public string Message;
    public NotFoundResponse(object id, string resource, string idField = "Id") : base(false, 404) =>
        Message = $"{resource} with {idField} {id} Not Found";
}
