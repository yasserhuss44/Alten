using Actio.Services.Activities.Handlers;
using Customers.Domain.Services;
using Customers.Infrastructure;
using Customers.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.API
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
            services.AddDbContext<CustomerContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("CustomersConnection")),ServiceLifetime.Singleton);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ICustomerVehicleRepository, CustomerVehiclesRepository>();
            services.AddHostedService<PullMessageTimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>builder.WithOrigins("http://localhost:4200"));
            app.UseMvc();

        }
    }
}
