using Common.Messaging.Commands;
using Customers.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class PullMessageTimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly ICustomerService _customerService;
        public PullMessageTimedHostedService(ILogger<PullMessageTimedHostedService> logger , ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(PullMessageFromServiceBus, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        private void PullMessageFromServiceBus(object state)
        {
            try
            {
                _logger.LogInformation("Pull Message From Service Bus Is Starting");

                string url = "amqp://eicyyvpq:uKcEpp7kaEpkHzDnIaeyAIvcVdkoYKr0@shark.rmq.cloudamqp.com/eicyyvpq";
                ConnectionFactory connFactory = new ConnectionFactory
                {
                    Uri = new Uri(url.Replace("amqp://", "amqps://"))
                };
                using (var conn = connFactory.CreateConnection())
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare("VehiclePing", false, false, false, null);
                    var data = channel.BasicGet("VehiclePing", false);
                    if (data == null)
                    {
                        _logger.LogInformation("No Messages Exist");
                        return;
                    }
                    var message = Encoding.UTF8.GetString(data.Body);
                    var command = Newtonsoft.Json.JsonConvert.DeserializeObject<PingVehicleCommand>(message);
                   var pingVehicleResponse=  this._customerService.PingVehicle(command);
                    if(pingVehicleResponse.StatusCode!=Helpers.Models.CommonEnums.ResponseStatusCode.Success)
                    {
                        _logger.LogInformation("Failed To Update Vehicle");
                        return;
                    }
                    channel.BasicAck(data.DeliveryTag, false);
                    _logger.LogInformation("Pull Message From Service Bus Is Completed");

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Pull Message From Service Aborted.. {ex.Message}");
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