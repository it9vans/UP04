using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UP04.Models;

public partial class InvoiceProduct
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public int ProductCount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
