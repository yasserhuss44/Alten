
using Helpers.Generics;
using Vehicles.Domain.Models;
using Helpers.Models;
using System.Threading.Tasks;

namespace Vehicles.Domain.Services
{
    public interface IVehicleService
    {
        ResponseDetails<VehicleDto> GetRandomVehicle();
    }
}