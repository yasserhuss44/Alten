using Common.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Infrastructure.Entities
{
   public  class CustomerVehicle:Entity
    {

        public CustomerVehicle( string vehicleId, string regNumber,string customerName, string customerAddress)
        {
            this.Owner = new Customer(customerName,customerAddress);
            this.VehicleId = vehicleId;
            this.RegNumber = regNumber;
        }
        public CustomerVehicle()
        {

        }
        public string VehicleId { get;   set; }

        public string RegNumber { get;  set; }

        public int CustomerId { get; set; }

        public DateTime? LastPingTime { get; set; }

        public Customer Owner{ get; set; }
    }
}
