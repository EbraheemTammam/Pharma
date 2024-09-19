namespace Pharmacy.Application.Responses;



public record OkResponse<ResultType>(ResultType Result) : BaseResponse(200);
