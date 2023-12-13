using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace UP04.Models;

public partial class Product
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string ShortName { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; } = null!;
    public int Count { get; set; }
    public long Price { get; set; }
    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();

}

