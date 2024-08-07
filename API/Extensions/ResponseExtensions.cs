using Pharmacy.Shared.Generics;

namespace Pharmacy.Presentation.Extensions;


public static class ResponseExtensions
{
    public static TResultType GetResult<TResultType>(this BaseResponse response) =>
        ((OkResponse<TResultType>)response).Result;
}
