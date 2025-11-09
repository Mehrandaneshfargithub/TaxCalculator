using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class CityTaxRule
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int Amount { get; set; }

    public virtual City City { get; set; } = null!;
}
