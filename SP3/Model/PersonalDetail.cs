using System;
using System.Collections.Generic;

namespace SP3.Model;

public partial class PersonalDetail
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public long? Cnic { get; set; }

    public bool? MarriageStatus { get; set; }

    public string? House { get; set; }

    public int? HouseAreaMarla { get; set; }

    public int? NoOfChildren { get; set; }

    public string? GrantCategory { get; set; }

    public int? MonthalyIncome { get; set; }

    public int? AvgMonthlyExpense { get; set; }

    public int? AvgMonthGasBill { get; set; }

    public int? AvgMonthElectricBill { get; set; }

    public string? JobType { get; set; }

    public virtual UserRegistry? CnicNavigation { get; set; }
}
