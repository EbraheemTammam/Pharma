using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Mappers;



public static class UserMapper
{
    public static User ToModel(this UserCreateDTO schema) =>
        new()
        {
            FirstName = schema.FirstName,
            LastName = schema.LastName,
            UserName = schema.Email
        };

    public static UserDTO ToDTO(this User model) =>
        new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.UserName!
        };

    public static void Update(this User user, UserCreateDTO schema)
    {
        user.FirstName = schema.FirstName;
        user.LastName = schema.LastName;
        user.Email = schema.Email;
        user.UserName = schema.Email;
    }
}
