namespace Pharmacy.Application.Responses;



public record ForbiddenResponse(string Message = "Permission denied") : BaseResponse(403);
