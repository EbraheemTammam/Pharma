using Pharmacy.Shared.Generics;

namespace Pharmacy.Application.Utilities;


public static class ResponseExtensions
{
    public static TResultType GetResult<TResultType>(this BaseResponse response) =>
        ((OkResponse<TResultType>)response).Result;
}
