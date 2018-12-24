using System;

namespace Common.Messaging.Events
{
    public interface IVehiclePingedEvent : IEvent
    {
         string VehicleId { get; }
    }
}