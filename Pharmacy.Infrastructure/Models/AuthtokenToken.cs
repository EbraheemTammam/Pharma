using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class AuthtokenToken
{
    public string Key { get; set; } = null!;

    public DateTime Created { get; set; }

    public long UserId { get; set; }

    public virtual AccountsCustomuser User { get; set; } = null!;
}
