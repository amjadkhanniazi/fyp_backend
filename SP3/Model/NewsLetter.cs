using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class NewsLetter
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;
}
