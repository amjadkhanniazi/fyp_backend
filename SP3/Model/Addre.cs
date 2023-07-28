using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class Addre
{
    public long? Cnic { get; set; }

    public int Id { get; set; }

    public string CurrentAddress { get; set; } = null!;

    public string PermanantAddress { get; set; } = null!;

    public string? NearbyFamousPlace { get; set; }

    public string? NearbyFamousPlace2 { get; set; }

    public string? Province { get; set; }

    public string? City { get; set; }

    public string? Town { get; set; }

    public int? PostalCode { get; set; }

    public virtual UserRegistry? CnicNavigation { get; set; }
}
