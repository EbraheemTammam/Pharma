namespace Pharmacy.Application.Responses;



public class NotFoundResponse : BaseResponse
{
    public string Message {get;}
    public NotFoundResponse(object id, string resource, string idField = "Id") : base(404) =>
        Message = $"{resource} with {idField} {id} Not Found";
}