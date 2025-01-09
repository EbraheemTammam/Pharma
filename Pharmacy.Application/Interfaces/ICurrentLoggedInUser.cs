using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Interfaces;

public interface ICurrentLoggedInUser
{
    string UserName { get; }
    Task<User> GetUser();
}
