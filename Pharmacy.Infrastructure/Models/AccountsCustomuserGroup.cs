using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class AccountsCustomuserGroup
{
    public long Id { get; set; }

    public long CustomuserId { get; set; }

    public int GroupId { get; set; }

    public virtual AccountsCustomuser Customuser { get; set; } = null!;

    public virtual AuthGroup Group { get; set; } = null!;
}
