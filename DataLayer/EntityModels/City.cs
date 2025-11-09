using System;
using System.Collections.Generic;

namespace DataLayer.EntityModels;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CityHoliday> CityHolidays { get; set; } = new List<CityHoliday>();

    public virtual ICollection<CityTaxRule> CityTaxRules { get; set; } = new List<CityTaxRule>();

    public virtual ICollection<CityVehicleTypeRule> CityVehicleTypeRules { get; set; } = new List<CityVehicleTypeRule>();

    public virtual ICollection<VehiclePassing> VehiclePassings { get; set; } = new List<VehiclePassing>();
}
