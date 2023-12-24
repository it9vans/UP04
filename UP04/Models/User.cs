using System;
using System.Collections.Generic;

namespace UP04.Models;

public partial class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

}
