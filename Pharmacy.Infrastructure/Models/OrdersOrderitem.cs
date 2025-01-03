using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class OrdersOrderitem
{
    public long Id { get; set; }

    public int Amount { get; set; }

    public double Price { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public virtual OrdersOrder Order { get; set; } = null!;

    public virtual ProductsType Product { get; set; } = null!;
}
