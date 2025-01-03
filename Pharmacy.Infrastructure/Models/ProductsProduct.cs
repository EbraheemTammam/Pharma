using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class ProductsProduct
{
    public long Id { get; set; }

    public DateOnly Expiration { get; set; }

    public int NumberOfElements { get; set; }

    public Guid IncomingOrderId { get; set; }

    public Guid TypeId { get; set; }

    public int NumberOfBoxes { get; set; }

    public virtual FinanceIncomingorder IncomingOrder { get; set; } = null!;

    public virtual ProductsType Type { get; set; } = null!;
}
