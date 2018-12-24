using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Customers.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerMock : ICustomerRepository
    {
        public DbSet<Customer> DbSet => throw new NotImplementedException();

        public void Add(Customer toAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer FindBy(Expression<Func<Customer, bool>> predicate, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Customer FindSingle(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAll(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = "")
        {
            return new List<Customer>
           {
               new Customer("Yasser","Assiut Egypt"),
           }.AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Customer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
