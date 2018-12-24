using Helpers.Generics;
using Customers.Domain.Models;
using Customers.Infrastructure.Entities;
using Customers.Infrastructure.Repositories;
using System.Linq;
using static Helpers.Models.CommonEnums;
using Helpers.Models;
using System;
using Common.Messaging.Commands;

namespace Customers.Domain.Services
{
    public class CustomerService:ICustomerService
    {
        ICustomerRepository _customerRepository;
        ICustomerVehicleRepository _customerVehicleRepository;
        public CustomerService(ICustomerRepository customerRepository,ICustomerVehicleRepository customerVehicleRepository)
        {
            this._customerRepository = customerRepository;
            this._customerVehicleRepository = customerVehicleRepository;
        }


        public ResponseDetailsList<CustomerVehicleAggregate> GetAllVehicles(string searchTage)
        {
            try
            {
                var customerVehicleQuery = this._customerVehicleRepository.DbSet.AsQueryable();
                if (!string.IsNullOrEmpty(searchTage) && searchTage != "all")
                {
                    var searchCustomerNameResult = customerVehicleQuery.Where(cv => cv.Owner.Name.ToLowerInvariant().Contains(searchTage.ToLowerInvariant()) || cv.Owner.Address.ToLowerInvariant().Contains(searchTage.ToLowerInvariant()));
                    var searchVehicleResult = customerVehicleQuery.Where(cv => cv.VehicleId.ToLowerInvariant().Contains(searchTage.ToLowerInvariant()) || cv.RegNumber.ToLowerInvariant().Contains(searchTage.ToLowerInvariant()));

                    customerVehicleQuery = searchCustomerNameResult.Union(searchVehicleResult);
                }

                var customerVehicleList = customerVehicleQuery.Select(cv => new CustomerVehicleAggregate
                {
                    Customer = new CustomerDto { Name = cv.Owner.Name, Address = cv.Owner.Address },
                    Vehicle = new VehicleDto { VehicleId = cv.VehicleId, RegNumber = cv.RegNumber, IsConnected = cv.LastPingTime.HasValue && DateTime.Now <= cv.LastPingTime.Value.AddMinutes(1) }
                }).ToList();
                if (customerVehicleList.Any())
                    return new ResponseDetailsList<CustomerVehicleAggregate>(customerVehicleList);
                return new ResponseDetailsList<CustomerVehicleAggregate>(ResponseStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return new ResponseDetailsList<CustomerVehicleAggregate>(ex);
            }
        }

        public ResponseDetailsBase PingVehicle(PingVehicleCommand pingVehicle)
        {
            var vehicle = this._customerVehicleRepository.DbSet.FirstOrDefault(v=>v.VehicleId.ToLowerInvariant()== pingVehicle.VehicleId.ToLowerInvariant());
            if (vehicle == null)
                return new ResponseDetailsBase(ResponseStatusCode.NotFound);
            vehicle.LastPingTime = pingVehicle.CreatedAt;
            vehicle.UpdatedOn = DateTime.Now;
            this._customerVehicleRepository.Update(vehicle);
            this._customerVehicleRepository.Save();
            return new ResponseDetailsList<CustomerVehicleAggregate>(ResponseStatusCode.Success);

        }

    }
}
