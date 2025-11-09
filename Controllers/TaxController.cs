using DataLayer.EntityModels;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models.City;
using TaxCalculator.Models;
using TaxCalculator.Models.Tax;
using System.Globalization;

namespace TaxCalculator.Controllers
{
    public class TaxController : Controller
    {
        private readonly IAllRepositories _diunit;
        public TaxController(IAllRepositories diunit)
        {
            _diunit = diunit;

        }

        [HttpGet]
        public IActionResult GetVehicleTax(GetTaxViewModel data)
        {
            try
            {
                var cityId = Convert.ToInt32(data.CityId);
                var vehicleId = Convert.ToInt32(data.VehicleId);
                var date = DateTime.ParseExact(data.Date,"yyyy/MM/dd", CultureInfo.InvariantCulture);

                int TotalTax = GetTax(cityId, vehicleId, date);

                return Json(TotalTax);
            }
            catch (Exception e)
            {
                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = e.Message
                });
            }

        }

        public int GetTax(int cityId, int vehicleId, DateTime selectedDate)
        {

            List<VehiclePassing> allPassings = _diunit.GetAllPassings(cityId, vehicleId, selectedDate);

            List<DateTime> dates = allPassings.Select(a => a.PassingDateTime).ToList();

            DateTime intervalStart = dates[0];
            int totalFee = 0;
            int maxFeeInInterval = 0;

            foreach (DateTime date in dates)
            {
                int currentFee = GetTollFee(date, vehicleId, cityId);
                var minutes = (date - intervalStart).TotalMinutes;

                if (minutes <= 60)
                {
                    if (currentFee > maxFeeInInterval)
                    {
                        maxFeeInInterval = currentFee;
                    }
                }
                else
                {
                    
                    totalFee += maxFeeInInterval;
                    intervalStart = date;
                    maxFeeInInterval = currentFee;
                }
            }

            totalFee += maxFeeInInterval;

            int maxDailyTax = 60;
            if (totalFee > maxDailyTax)
                totalFee = maxDailyTax;

            return totalFee;


            //DateTime intervalStart = dates[0];
            //int totalFee = 0;
            //foreach (DateTime date in dates)
            //{
            //    int nextFee = GetTollFee(date, vehicleId);
            //    int tempFee = GetTollFee(intervalStart, vehicleId);

            //    //long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            //    //long minutes = diffInMillies / 1000 / 60;

            //    var minutes = (date - intervalStart).TotalMinutes;

            //    if (minutes <= 60)
            //    {
            //        //if (totalFee > 0) totalFee -= tempFee;
            //        if (nextFee >= tempFee) tempFee = nextFee;
            //        totalFee += tempFee;
            //    }
            //    else
            //    {
            //        totalFee += nextFee;
            //    }
            //}
            //if (totalFee > 60) totalFee = 60;
            //return totalFee;
        }

        private bool IsTollFreeVehicle(int? vehicleId,int cityId)
        {
            if (vehicleId == null) return false;
            //String vehicleType = vehicle.GetVehicleType();
            //return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Military.ToString());

            var allTypes = _diunit.GetAllCityVehicleTypes(cityId);

            Vehicle? vehicle = _diunit.GetVehicleById(vehicleId);

            if (vehicle == null) return false;

            return allTypes.Any(a => a.Id == vehicle.VehicleTypeId);

            

        }

        public int GetTollFee(DateTime date, int vehicleId, int cityId)
        {

            if (IsTollFreeDate(date, cityId) || IsTollFreeVehicle(vehicleId, cityId))
                return 0;

            List<CityTaxRule> taxRules = _diunit.GetTaxRulesByCity(cityId);
            TimeOnly timeOfDay = TimeOnly.FromDateTime(date);

            foreach (var rule in taxRules)
            {
                if (timeOfDay >= rule.StartTime && timeOfDay <= rule.EndTime)
                {
                    return rule.Amount;
                }
            }

            return 0;



            //if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicleId)) return 0;

            //int hour = date.Hour;
            //int minute = date.Minute;

            //if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            //else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            //else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            //else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            //else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            //else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            //else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            //else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            //else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            //else return 0;
        }

        private Boolean IsTollFreeDate(DateTime date, int cityId)
        {

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            if (date.Month == 7)
                return true;

            List<DateTime> holidays = _diunit.GetHolidaysByCity(cityId, date.Year);

            foreach (var holiday in holidays)
            {
                
                if (holiday.Date.Date == date.Date)
                    return true;

                
                if (holiday.Date.Date.AddDays(-1) == date.Date)
                    return true;
            }

            return false;



            //int year = date.Year;
            //int month = date.Month;
            //int day = date.Day;

            //if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            //if (year == 2013)
            //{
            //    if (month == 1 && day == 1 ||
            //        month == 3 && (day == 28 || day == 29) ||
            //        month == 4 && (day == 1 || day == 30) ||
            //        month == 5 && (day == 1 || day == 8 || day == 9) ||
            //        month == 6 && (day == 5 || day == 6 || day == 21) ||
            //        month == 7 ||
            //        month == 11 && day == 1 ||
            //        month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

    }
}
