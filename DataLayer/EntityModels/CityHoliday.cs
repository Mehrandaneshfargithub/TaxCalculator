using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class CityHoliday
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public DateTime HolidayDate { get; set; }

    public virtual City City { get; set; } = null!;
}
