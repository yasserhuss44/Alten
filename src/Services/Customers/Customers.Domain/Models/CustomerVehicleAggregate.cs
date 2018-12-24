using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Domain.Models
{
   public class CustomerVehicleAggregate
    {
        public CustomerDto Customer { get; set; }

        public VehicleDto Vehicle { get; set; }

    }
}
