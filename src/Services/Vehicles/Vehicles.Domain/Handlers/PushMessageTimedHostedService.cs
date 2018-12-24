using Common.Messaging.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vehicles.Domain.Services;

namespace Vehicles.Domain.Handlers
{
    public class PushMessageTimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        IVehicleService _vehicleService;

        public PushMessageTimedHostedService(ILogger<PushMessageTimedHostedService> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(Ping, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        private void Ping(object state)
        {
            try
            {
                _logger.LogInformation("Push Message To Service Bus is starting");

                var getRandomVehicle = _vehicleService.GetRandomVehicle();
                if (getRandomVehicle.StatusCode != Helpers.Models.CommonEnums.ResponseStatusCode.Success)
                {
                    _logger.LogInformation("Cannot Retrieve Random Vehicle");
                    return;
                }
                var pingComm = new PingVehicleCommand()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    VehicleId = getRandomVehicle.DetailsObject.VehicleId
                };
                string url = "amqp://eicyyvpq:uKcEpp7kaEpkHzDnIaeyAIvcVdkoYKr0@shark.rmq.cloudamqp.com/eicyyvpq";
                ConnectionFactory factory = new ConnectionFactory
                {
                    Uri = new Uri(url.Replace("amqp://", "amqps://"))
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "VehiclePing",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(pingComm);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "VehiclePing",
                                         basicProperties: null,
                                         body: body);
                    _logger.LogInformation("Push Message To Service Bus Comleted");

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}