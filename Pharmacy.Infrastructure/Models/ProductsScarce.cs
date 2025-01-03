using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models;

public partial class ProductsScarce
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }
}
