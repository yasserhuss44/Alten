using System;

namespace Common.Messaging.Events
{
    public class VehiclePinged : IVehiclePingedEvent
    {
        public Guid Id { get; }
        public string VehicleId { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

        protected VehiclePinged()
        {
        }

        public VehiclePinged(Guid id, string vehicleId,
            string description, DateTime createdAt)
        {
            Id = id;
            VehicleId = vehicleId;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}