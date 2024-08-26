using Pharmacy.Shared.Responses;

namespace Pharmacy.Presentation.Utilities;


public static class ResponseExtensions
{
    public static TResultType GetResult<TResultType>(this BaseResponse response) =>
        ((OkResponse<TResultType>)response).Result;
}
