using Common.Messaging.Commands;
using Helpers.Models;
using RabbitMQ.Client;
using RawRabbit;
using System;
using System.Linq;
using System.Text;
using Vehicles.Domain.Models;
using Vehicles.Infrastructure.Entities;
using Vehicles.Infrastructure.Repositories;
using static Helpers.Models.CommonEnums;

namespace Vehicles.Domain.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _VehicleRepository;
        private readonly IBusClient _busClient;

        public VehicleService(IVehicleRepository VehicleRepository, IBusClient busClient)
        {
            _VehicleRepository = VehicleRepository;
            _busClient = busClient;
        }

        public ResponseDetails<VehicleDto> GetRandomVehicle()
        {
            var vehicle = _VehicleRepository.DbSet.OrderBy(_ => Guid.NewGuid()).Select(v=>new VehicleDto() { RegNumber=v.RegNumber,VehicleId=v.VehicleId}).Take(1).FirstOrDefault();
            if (vehicle == null)
                return new ResponseDetails<VehicleDto>(ResponseStatusCode.ServerError);
            return new ResponseDetails<VehicleDto>(vehicle);
        }
    }
}
