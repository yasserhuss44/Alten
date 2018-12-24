using Customers.Infrastructure.Entities;
using Helpers.Generics;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerVehiclesRepository:GenericRepository<CustomerVehicle>, ICustomerVehicleRepository
    {
        public CustomerVehiclesRepository(CustomerContext customerContext) : base(customerContext) { } 
    }
}
