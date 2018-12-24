using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Services.Activities.Handlers;
using Common.Messaging.Commands;
using Common.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vehicles.Domain.Handlers;
using Vehicles.Domain.Services;
using Vehicles.Infrastructure;
using Vehicles.Infrastructure.Repositories;

namespace Vehicles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VehicleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VehicleConnection")),ServiceLifetime.Singleton);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddRabbitMq(Configuration);
            services.AddHostedService<PushMessageTimedHostedService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
