using System;
using System.Collections.Generic;

namespace UP04.Models;

public partial class Warehouse
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User WarehouseWorker { get; set; } = null!;

}
