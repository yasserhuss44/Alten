using System;

namespace Common.Messaging.Commands
{
    public class PingVehicleCommand : IPingVehicleCommand
    {
        public Guid Id { get; set; }
        public string VehicleId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}