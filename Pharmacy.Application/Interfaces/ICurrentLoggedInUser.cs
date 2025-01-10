using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Interfaces;

public interface ICurrentLoggedInUser
{
    string UserId { get; }
    Task<User> GetUser();
}
