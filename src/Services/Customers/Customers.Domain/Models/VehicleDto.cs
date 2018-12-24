using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Domain.Models
{
    public class VehicleDto
    {
        public string VehicleId { get; set; }

        public string RegNumber { get; set; }

        public bool IsConnected { get; set; }

    }
}
