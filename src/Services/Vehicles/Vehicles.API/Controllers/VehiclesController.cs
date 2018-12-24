using Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vehicles.Domain.Models;
using Vehicles.Domain.Services;

namespace Vehicles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private IVehicleService _vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        
        
    }
}
