using Customers.Domain.Models;
using Customers.Domain.Services;
using Helpers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("GetAllVehicles/{searchTage}", Name = "GetAllVehicles")]
        public ActionResult<ResponseDetailsList<CustomerVehicleAggregate>> GetAllVehicles(string searchTage)
        {
            return _customerService.GetAllVehicles(searchTage);
        }
   
    }
}
