using Pharmacy.Domain.Modules.Users.Models;
using Pharmacy.Shared.Modules.Users.DTOs;

namespace Pharmacy.Application.Modules.Users.Mappers;



public static class UserMapper
{
    public static CustomUser ToModel(this UserCreateDTO schema) =>
        new()
        {
            FirstName = schema.FirstName,
            LastName = schema.LastName,
            UserName = schema.Email
        };

    public static UserDTO ToDTO(this CustomUser model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.UserName!
        };

    public static void Update(this CustomUser user, UserCreateDTO schema)
    {
        user.FirstName = schema.FirstName;
        user.LastName = schema.LastName;
        user.Email = schema.Email;
        user.UserName = schema.Email;
    }
}
