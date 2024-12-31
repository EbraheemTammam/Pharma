using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Interfaces;

public interface ICurrentLoggedInUser
{
    Guid UserId { get; }
    string Email { get; }
    Task<User> GetUser();
}
