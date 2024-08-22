namespace Pharmacy.Shared.Generics;



public class OkResponse<ResultType> : BaseResponse
{
    public ResultType Result;
    public OkResponse(ResultType result) : base(200) =>
        Result = result;
}
