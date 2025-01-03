using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class FinanceIncomingorder
{
    public Guid Id { get; set; }

    public DateTime Time { get; set; }

    public double Price { get; set; }

    public double Paid { get; set; }

    public Guid CompanyId { get; set; }

    public virtual FinanceCompany Company { get; set; } = null!;

    public virtual ICollection<ProductsProduct> ProductsProducts { get; set; } = new List<ProductsProduct>();
}
