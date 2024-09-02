namespace Pharmacy.Application.Responses;



public class CreatedResponse<T> : BaseResponse
{
    public T Data { get; set;}
    public CreatedResponse(T data) : base(201) => Data = data;
}
