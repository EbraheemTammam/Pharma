using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class AccountsCustomuserUserPermission
{
    public long Id { get; set; }

    public long CustomuserId { get; set; }

    public int PermissionId { get; set; }

    public virtual AccountsCustomuser Customuser { get; set; } = null!;

    public virtual AuthPermission Permission { get; set; } = null!;
}
