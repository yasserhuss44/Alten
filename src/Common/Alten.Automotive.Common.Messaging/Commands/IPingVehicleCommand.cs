using System;

namespace Common.Messaging.Commands
{
    public interface IPingVehicleCommand : ICommand
    {
         string VehicleId { get; set; }
    }
}