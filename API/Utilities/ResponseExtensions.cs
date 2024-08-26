using Pharmacy.Shared.Responses;

namespace Pharmacy.Application.Utilities;


public static class ResponseExtensions
{
    public static TResultType GetResult<TResultType>(this BaseResponse response) =>
        ((OkResponse<TResultType>)response).Result;
}
