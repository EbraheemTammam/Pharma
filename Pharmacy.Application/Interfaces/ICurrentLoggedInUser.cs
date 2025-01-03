using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Interfaces;

public interface ICurrentLoggedInUser
{
    string Email { get; }
    Task<User> GetUser();
}
