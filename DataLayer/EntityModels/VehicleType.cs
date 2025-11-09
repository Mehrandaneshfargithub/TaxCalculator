using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class VehicleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CityVehicleTypeRule> CityVehicleTypeRules { get; set; } = new List<CityVehicleTypeRule>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
