using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public string LicensePlate { get; set; } = null!;

    public int VehicleTypeId { get; set; }

    public virtual ICollection<VehiclePassing> VehiclePassings { get; set; } = new List<VehiclePassing>();

    public virtual VehicleType VehicleType { get; set; } = null!;
}
