using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;

public static class UserMapper
{
    public static User ToUserModel(this RegisterDTO schema) =>
        new()
        {
            FirstName = schema.FirstName,
            LastName = schema.LastName,
            UserName = schema.Username
        };

    public static UserDTO ToUserDTO(this User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName!
        };

    public static void Update(this User user, RegisterDTO schema)
    {
        user.FirstName = schema.FirstName;
        user.LastName = schema.LastName;
        user.Email = schema.Username + "@pharmacy.com";
        user.UserName = schema.Username;
    }
}
