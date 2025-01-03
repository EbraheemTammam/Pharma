using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class OrdersOrder
{
    public Guid Id { get; set; }

    public DateTime Time { get; set; }

    public double Paid { get; set; }

    public double TotalPrice { get; set; }

    public Guid? CustomerId { get; set; }

    public long UserId { get; set; }

    public virtual OrdersCustomer? Customer { get; set; }

    public virtual ICollection<OrdersOrderitem> OrdersOrderitems { get; set; } = new List<OrdersOrderitem>();

    public virtual AccountsCustomuser User { get; set; } = null!;
}
