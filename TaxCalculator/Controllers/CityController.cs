using DataLayer.EntityModels;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TaxCalculator.Models;
using TaxCalculator.Models.City;
using TaxCalculator.Models.CityHoliday;
using TaxCalculator.Models.CityTax;
using TaxCalculator.Models.CityVehicle;

namespace TaxCalculator.Controllers
{
    public class CityController : Controller
    {
        private readonly IAllRepositories _diunit;
        public CityController(IAllRepositories diunit)
        {
            _diunit = diunit;
            
        }


        [HttpPost]
        public IActionResult AddCity(string cityName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = false,
                        Message = "Fill The City Name"
                    });
                }

                string status = _diunit.AddCity(cityName);

                if(status.ToLower().Trim() == "success")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "City Added Successfully"
                    });
                }

                if (status.ToLower().Trim() == "cityexist")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "City Exist"
                    });
                }

                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = "City Not Added"
                });

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


        [HttpGet]
        public IActionResult GetAllCities()
        {
            try
            {

                IEnumerable<City> result = _diunit.GetAllCities();

                IEnumerable<CityViewModel> allCities = result.Select(a => new CityViewModel
                {
                    Id = a.Id.ToString(),
                    Name = a.Name
                });

                return Json(new ResponseViewModel<IEnumerable<CityViewModel>>
                {
                    Success = true,
                    Data = allCities
                });

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


        [HttpPost]
        public IActionResult AddCityHoliday(CityHolidayViewModel cityHoliday)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityHoliday.HolidayDate))
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = false,
                        Message = "Fill The Date"
                    });
                }

                CityHoliday CH = new CityHoliday()
                {
                    CityId = Convert.ToInt32(cityHoliday.CityId),
                    HolidayDate = DateTime.ParseExact(cityHoliday.HolidayDate, "yyyy/MM/dd", CultureInfo.InvariantCulture)
                };

                string status = _diunit.AddCityHoliday(CH);

                if (status.ToLower().Trim() == "success")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "City Holiday Added Successfully"
                    });
                }

                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = "City Holiday Not Added"
                });

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


        [HttpPost]
        public IActionResult AddCityVehicle(CityVehicleViewModel cityVehicle)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityVehicle.VehicleTypeId))
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = false,
                        Message = "Fill The VehicleType"
                    });
                }

                CityVehicleTypeRule CV = new CityVehicleTypeRule()
                {
                    CityId = Convert.ToInt32(cityVehicle.CityId),
                    VehicleTypeId = Convert.ToInt32(cityVehicle.VehicleTypeId),
                };

                string status = _diunit.AddCityVehicle(CV);

                if (status.ToLower().Trim() == "success")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "City Vehicle Added Successfully"
                    });
                }

                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = "City Holiday Not Added"
                });

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

        [HttpPost]
        public IActionResult AddCityTax(CityTaxViewModel cityTax)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityTax.StartTime)||
                    string.IsNullOrWhiteSpace(cityTax.EndTime)||
                    string.IsNullOrWhiteSpace(cityTax.Amount))
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = false,
                        Message = "Fill All Inputs"
                    });
                }

                CityTaxRule CT = new CityTaxRule()
                {
                    CityId = Convert.ToInt32(cityTax.CityId),
                    Amount = Convert.ToInt32(cityTax.Amount),
                    StartTime = TimeOnly.ParseExact(cityTax.StartTime,"HH:mm",CultureInfo.InvariantCulture),
                    EndTime = TimeOnly.ParseExact(cityTax.EndTime,"HH:mm",CultureInfo.InvariantCulture)
                };

                string status = _diunit.AddCityTax(CT);

                if (status.ToLower().Trim() == "success")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "City Vehicle Added Successfully"
                    });
                }

                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = "City Holiday Not Added"
                });

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

    }
}
