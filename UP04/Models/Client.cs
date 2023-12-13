using System;
using System.Collections.Generic;

namespace UP04.Models;

public partial class Client
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;


}
