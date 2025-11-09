using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class VehiclePassing
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int CityId { get; set; }

    public DateTime PassingDateTime { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
