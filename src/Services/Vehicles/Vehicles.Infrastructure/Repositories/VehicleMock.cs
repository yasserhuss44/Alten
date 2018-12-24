using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Vehicles.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Vehicles.Infrastructure.Repositories
{
    public class VehicleMock : IVehicleRepository
    {
        public DbSet<Vehicle> DbSet => throw new NotImplementedException();

        public void Add(Vehicle toAdd)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Vehicle FindBy(Expression<Func<Vehicle, bool>> predicate, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Vehicle FindSingle(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Vehicle> GetAll(Expression<Func<Vehicle, bool>> filter = null, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy = null, string includeProperties = "")
        {
            return new List<Vehicle>
           {
               new Vehicle("YS2R4X20005399401","ABC123"),
           }.AsQueryable();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Vehicle toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
