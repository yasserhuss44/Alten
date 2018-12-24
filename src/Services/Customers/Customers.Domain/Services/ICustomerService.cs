using Helpers.Generics;
using Customers.Domain.Models;
using Helpers.Models;
using Common.Messaging.Commands;

namespace Customers.Domain.Services
{
    public interface ICustomerService
    {

        ResponseDetailsList<CustomerVehicleAggregate> GetAllVehicles(string searchTage);

        ResponseDetailsBase PingVehicle(PingVehicleCommand pingVehicle);
        
                    
    }
}