using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class UserDocument
{
    public int Id { get; set; }

    public long? Cnic { get; set; }

    public bool? CnicFront { get; set; }

    public bool? CnicBack { get; set; }

    public bool? ElectricBill { get; set; }

    public bool? GasBill { get; set; }

    public virtual UserRegistry? CnicNavigation { get; set; }
}
