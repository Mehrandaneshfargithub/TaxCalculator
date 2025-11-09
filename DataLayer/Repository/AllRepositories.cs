using DataLayer.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class AllRepositories : IAllRepositories
    {

        protected readonly DataContext Context;

        public AllRepositories(DataContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //////////////////City Repository///////////////
        public string AddCity(string cityName)
        {

            if(Context.Cities.Any(a=>a.Name == cityName))
            {
                return "City Exist";
            }

            Context.Cities.Add(new City
            {
                Name = cityName
            });

            Context.SaveChanges();

            return "Success";

        }

        public IEnumerable<City> GetAllCities()
        {
            return Context.Cities.AsNoTracking().Select(a => new City
            {
                Id = a.Id,
                Name = a.Name
            });
        }

        public List<DateTime> GetHolidaysByCity(int cityId, int year)
        {
            return Context.CityHolidays.AsNoTracking().Where(a => a.CityId == cityId).Select(a => a.HolidayDate).ToList();
        }

        /////////////////////////////////////////////////////////
        //////////////////CityHoliday Repository///////////////
        
        public string AddCityHoliday(CityHoliday cH)
        {
            Context.CityHolidays.Add(cH);

            Context.SaveChanges();

            return "Success";
        }

        /////////////////////////////////////////////////////////
        //////////////////CityTaxRule Repository///////////////
        
        public string AddCityTax(CityTaxRule cT)
        {
            Context.CityTaxRules.Add(cT);

            Context.SaveChanges();

            return "Success";
        }

        /////////////////////////////////////////////////////////
        //////////////////CityVehicleTypeRule Repository///////////////
        
        public string AddCityVehicle(CityVehicleTypeRule cV)
        {
            Context.CityVehicleTypeRules.Add(cV);

            Context.SaveChanges();

            return "Success";
        }


        public IEnumerable<CityVehicleTypeRule> GetAllCityVehicleTypes(int cityId)
        {
            return Context.CityVehicleTypeRules.AsNoTracking().Where(a => a.CityId == cityId).Select(a => new CityVehicleTypeRule
            {
                Id = a.Id,
                VehicleTypeId = a.VehicleTypeId
            });
        }


        /////////////////////////////////////////////////////////
        //////////////////VehiclePassing Repository///////////////

        public string AddVehiclePassing(VehiclePassing vP)
        {
            Context.VehiclePassings.Add(vP);
            Context.SaveChanges();
            return "Success";
        }

        public List<VehiclePassing> GetAllPassings(int cityId, int vehicleId, DateTime date)
        {
            return Context.VehiclePassings
                .AsNoTracking()
                .Where(a=>a.VehicleId == vehicleId && a.CityId == cityId && a.PassingDateTime < date.AddDays(1) && a.PassingDateTime > date)
                .Select(a => new VehiclePassing
                {
                    PassingDateTime = a.PassingDateTime
                }).OrderBy(a=>a.PassingDateTime).ToList();
        }

        /////////////////////////////////////////////////////////
        //////////////////Vehicle Repository///////////////
        
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return Context.Vehicles.AsNoTracking().Select(a => new Vehicle
            {
                Id = a.Id,
                LicensePlate = a.LicensePlate
            });
        }

        public Vehicle? GetVehicle(string licensePlate)
        {
            return Context.Vehicles.AsNoTracking().Where(a => a.LicensePlate == licensePlate).FirstOrDefault();
        }

        public Vehicle? GetVehicleById(int? vehicleId)
        {
            return Context.Vehicles.AsNoTracking().Where(a => a.Id == vehicleId).FirstOrDefault();
        }

        /////////////////////////////////////////////////////////
        //////////////////CityTaxRule Repository///////////////

        public List<CityTaxRule> GetTaxRulesByCity(int cityId)
        {
            return Context.CityTaxRules.AsNoTracking().Where(a => a.CityId == cityId).ToList();
        }

        /////////////////////////////////////////////////////////
        //////////////////VehicleType Repository///////////////

        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            return Context.VehicleTypes.AsNoTracking().Select(a => new VehicleType
            {
                Id = a.Id,
                Name = a.Name
            });
        }
    }
}
