using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class NeedyBankDetail
{
    public int Id { get; set; }

    public long? Cnic { get; set; }

    public string? Iban { get; set; }

    public string? AccountTitle { get; set; }

    public string? BankName { get; set; }

    public virtual UserRegistry? CnicNavigation { get; set; }
}
