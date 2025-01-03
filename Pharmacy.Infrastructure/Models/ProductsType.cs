using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class ProductsType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfElements { get; set; }

    public double PricePerElement { get; set; }

    public string? Barcode { get; set; }

    public int OwnedElements { get; set; }

    public bool Lack { get; set; }

    public int Minimum { get; set; }

    public virtual ICollection<OrdersOrderitem> OrdersOrderitems { get; set; } = new List<OrdersOrderitem>();

    public virtual ICollection<ProductsProduct> ProductsProducts { get; set; } = new List<ProductsProduct>();
}
