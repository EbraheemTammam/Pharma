using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class FinanceCompany
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FinanceIncomingorder> FinanceIncomingorders { get; set; } = new List<FinanceIncomingorder>();
}
