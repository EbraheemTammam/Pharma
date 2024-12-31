using Microsoft.AspNetCore.Http;

namespace Pharmacy.Application.Responses;

public static class AppResponses
{
    public static Response BadRequestResponse(string message) => new Response(StatusCodes.Status400BadRequest, message);
    public static Response NotFoundResponse(object id, string resource, string idField = "Id") => new Response(StatusCodes.Status404NotFound, $"{resource} with {idField} {id} Not Found");
    public static Response InternalServerResponse = new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
    public static Response UnAuthorizedResponse = new Response(StatusCodes.Status401Unauthorized, "Email or password is incorrect");
    public static Response ForbiddenResponse = new Response(StatusCodes.Status403Forbidden, "Permission denied");
}
