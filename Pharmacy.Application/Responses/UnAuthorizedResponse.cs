namespace Pharmacy.Application.Responses;



public record UnAuthorizedResponse(string Message = "Email or password is incorrect") : BaseResponse(401);
