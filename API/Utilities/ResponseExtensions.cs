using Pharmacy.Shared.Responses;

namespace Pharmacy.Presentation.Utilities;


public static class ResponseExtensions
{
    public static TResultType GetResult<TResultType>(this BaseResponse response) =>
        ((OkResponse<TResultType>)response).Result;
    public static TResultType GetData<TResultType>(this BaseResponse response) =>
        ((CreatedResponse<TResultType>)response).Data;
}
