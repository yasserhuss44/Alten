using Helpers.Generics;
using Customers.Infrastructure.Entities;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>,ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext) : base(customerContext)
        {
            
        }

        
    }
}
