using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Customers.Infrastructure.Entities;

namespace Customers.Infrastructure
{
    public class CustomerContext : DbContext
    {
     
        public CustomerContext(DbContextOptions<CustomerContext> dbContextOptions)
        : base(dbContextOptions)
        {
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerVehicle> CustomerVehicles { get; set; }

    }
}
