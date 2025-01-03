using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class OrdersPayment
{
    public long Id { get; set; }

    public double Paid { get; set; }

    public DateTime Time { get; set; }

    public Guid CustomerId { get; set; }

    public virtual OrdersCustomer Customer { get; set; } = null!;
}
