using System;
using System.Collections.Generic;

namespace UP04.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();


}
