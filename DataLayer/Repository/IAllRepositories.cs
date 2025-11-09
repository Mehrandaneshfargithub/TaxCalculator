using DataLayer.EntityModels;

namespace DataLayer.Repository
{
    public interface IAllRepositories
    {
        string AddCity(string cityName);
        string AddCityHoliday(CityHoliday cH);
        string AddCityTax(CityTaxRule cT);
        string AddCityVehicle(CityVehicleTypeRule cV);
        string AddVehiclePassing(VehiclePassing vP);
        IEnumerable<City> GetAllCities();
        List<VehiclePassing> GetAllPassings(int cityId, int vehicleId, DateTime date);
        IEnumerable<Vehicle> GetAllVehicles();
        IEnumerable<CityVehicleTypeRule> GetAllCityVehicleTypes(int cityId);
        List<DateTime> GetHolidaysByCity(int cityId, int year);
        List<CityTaxRule> GetTaxRulesByCity(int cityId);
        Vehicle? GetVehicle(string licensePlate);
        Vehicle? GetVehicleById(int? vehicleId);
        IEnumerable<VehicleType> GetAllVehicleTypes();
    }
}
