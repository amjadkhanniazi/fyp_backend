using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class UserRegistry
{
    public long Cnic { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Addre> Addres { get; set; } = new List<Addre>();

    public virtual ICollection<NeedyBankDetail> NeedyBankDetails { get; set; } = new List<NeedyBankDetail>();

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();

    public virtual ICollection<UserDocument> UserDocuments { get; set; } = new List<UserDocument>();
}
