using System;
using System.Collections.Generic;

namespace UP04.Models;

public partial class Invoice
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public long TotalPrice { get; set; }
    public int WarehouseWorkerId { get; set; }
    public User WarehouseWorker { get; set; } = null!;
    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();

}
