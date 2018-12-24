using Helpers.Generics;
using Customers.Infrastructure.Entities;
using Helpers.Models;

namespace Customers.Infrastructure.Repositories
{
    public  interface ICustomerRepository:IGenericRepository<Customer>
    {
    }
}