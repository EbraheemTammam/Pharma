using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class AccountsCustomuser
{
    public long Id { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public bool IsSuperuser { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsStaff { get; set; }

    public bool IsActive { get; set; }

    public DateTime DateJoined { get; set; }

    public bool IsAdmin { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<AccountsCustomuserGroup> AccountsCustomuserGroups { get; set; } = new List<AccountsCustomuserGroup>();

    public virtual ICollection<AccountsCustomuserUserPermission> AccountsCustomuserUserPermissions { get; set; } = new List<AccountsCustomuserUserPermission>();

    public virtual AuthtokenToken? AuthtokenToken { get; set; }

    public virtual ICollection<DjangoAdminLog> DjangoAdminLogs { get; set; } = new List<DjangoAdminLog>();

    public virtual ICollection<OrdersOrder> OrdersOrders { get; set; } = new List<OrdersOrder>();
}
