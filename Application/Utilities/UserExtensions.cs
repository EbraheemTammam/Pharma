using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Utilities;



internal static class UserExtensions
{
    public static string GetFullName(this User user) => $"{user.FirstName} {user.LastName}";
}
