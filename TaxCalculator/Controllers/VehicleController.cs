using DataLayer.EntityModels;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Models.City;
using TaxCalculator.Models.Vehicle;
using TaxCalculator.Views.City;

namespace TaxCalculator.Controllers
{
    public class VehicleController : Controller
    {

        private readonly IAllRepositories _diunit;
        public VehicleController(IAllRepositories diunit)
        {
            _diunit = diunit;

        }

        [HttpGet]
        public IActionResult GetAllVehicleTypes()
        {
            try
            {

                IEnumerable<VehicleType> result = _diunit.GetAllVehicleTypes();

                IEnumerable<VehicleTypeViewModel> allTypes = result.Select(a => new VehicleTypeViewModel
                {
                    Id = a.Id.ToString(),
                    Name = a.Name
                });

                return Json(new ResponseViewModel<IEnumerable<VehicleTypeViewModel>>
                {
                    Success = true,
                    Data = allTypes
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
        public IActionResult GetAllVehicles()
        {
            try
            {

                IEnumerable<Vehicle> result = _diunit.GetAllVehicles();

                IEnumerable<VehicleViewModel> allVehicle = result.Select(a => new VehicleViewModel
                {
                    Id = a.Id.ToString(),
                    LicensePlate = a.LicensePlate
                });

                return Json(new ResponseViewModel<IEnumerable<VehicleViewModel>>
                {
                    Success = true,
                    Data = allVehicle
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
