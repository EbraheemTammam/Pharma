namespace Pharmacy.Application.Responses;



public record CreatedResponse<T>(T Data) : BaseResponse(201);
