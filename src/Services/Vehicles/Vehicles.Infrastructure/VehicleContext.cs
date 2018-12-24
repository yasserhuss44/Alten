using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Vehicles.Infrastructure.Entities;

namespace Vehicles.Infrastructure
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> dbContextOptions)
        : base(dbContextOptions)
        {
        }
        

        public DbSet<Vehicle> Vehicles { get; set; }
        
    }
}
