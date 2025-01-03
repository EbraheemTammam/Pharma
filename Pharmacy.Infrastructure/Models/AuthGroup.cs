using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class AuthGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccountsCustomuserGroup> AccountsCustomuserGroups { get; set; } = new List<AccountsCustomuserGroup>();

    public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; } = new List<AuthGroupPermission>();
}
