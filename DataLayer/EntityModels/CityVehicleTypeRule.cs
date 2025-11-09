using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class CityVehicleTypeRule
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public int VehicleTypeId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual VehicleType VehicleType { get; set; } = null!;
}
