using Common.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Infrastructure.Entities
{
    public class Vehicle: Entity
    {
        public string VehicleId { get; private set; }

        public string RegNumber { get; private set; }

        public Vehicle(string vehicleId,string regNumber)
        {
            this.VehicleId = vehicleId;
            this.RegNumber = regNumber;
        }
    }
}
