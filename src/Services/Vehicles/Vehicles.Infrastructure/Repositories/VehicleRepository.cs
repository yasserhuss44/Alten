using Helpers.Generics;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Entities;

namespace Vehicles.Infrastructure.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(VehicleContext vehicleContext) : base(vehicleContext)
        {

        }
    }
}
