using DataLayer.EntityModels;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TaxCalculator.Models;
using TaxCalculator.Models.VehiclePassing;

namespace TaxCalculator.Controllers
{
    public class VehiclePassingController : Controller
    {
        private readonly IAllRepositories _diunit;
        public VehiclePassingController(IAllRepositories diunit)
        {
            _diunit = diunit;

        }

        [HttpPost]
        public IActionResult AddVehiclePassing(VehiclePassingViewModel data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.PassingDateTime) ||
                    string.IsNullOrWhiteSpace(data.LicensePlate) || 
                    string.IsNullOrWhiteSpace(data.CityId) || 
                    string.IsNullOrWhiteSpace(data.VehicleTypeId))
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = false,
                        Message = "Fill All Inputs"
                    });
                }

                Vehicle? oldVehicle = _diunit.GetVehicle(data.LicensePlate);

                VehiclePassing VP = new();

                if(oldVehicle == null)
                {
                    VP = new VehiclePassing
                    {
                        CityId = Convert.ToInt32(data.CityId),
                        PassingDateTime = DateTime.ParseExact(data.PassingDateTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        Vehicle = new Vehicle
                        {
                            LicensePlate = data.LicensePlate,
                            VehicleTypeId = Convert.ToInt32(data.VehicleTypeId)
                        }
                    };
                }
                else
                {
                    VP = new VehiclePassing
                    {
                        CityId = Convert.ToInt32(data.CityId),
                        PassingDateTime = DateTime.ParseExact(data.PassingDateTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        VehicleId = oldVehicle.Id
                    };
                }


                string status = _diunit.AddVehiclePassing(VP);

                if (status.ToLower().Trim() == "success")
                {
                    return Json(new ResponseViewModel<object>
                    {
                        Success = true,
                        Message = "Added Successfully"
                    });
                }

                return Json(new ResponseViewModel<object>
                {
                    Success = false,
                    Message = "Not Added"
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
