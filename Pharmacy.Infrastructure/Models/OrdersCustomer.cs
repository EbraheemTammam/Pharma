using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class OrdersCustomer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public double Dept { get; set; }

    public virtual ICollection<OrdersOrder> OrdersOrders { get; set; } = new List<OrdersOrder>();

    public virtual ICollection<OrdersPayment> OrdersPayments { get; set; } = new List<OrdersPayment>();
}
