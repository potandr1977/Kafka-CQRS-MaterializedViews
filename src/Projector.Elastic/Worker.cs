using EventBus.Kafka.Abstraction;
using Messages.Account;
using Messages.Payment;
using Messages.Person;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Projector.Elastic
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IKafkaConsumer<UpdateAccountProjectionMessage> _kafkaAccountConsumer;
        private readonly IKafkaConsumer<UpdatePaymentProjectionMessage> _kafkaPaymentConsumer;
        private readonly IKafkaConsumer<UpdatePersonProjectionMessage> _kafkaPersonConsumer;

        public Worker(
            IKafkaConsumer<UpdateAccountProjectionMessage> kafkaAccountConsumer,
            IKafkaConsumer<UpdatePaymentProjectionMessage> kafkaPaymentConsumer,
            IKafkaConsumer<UpdatePersonProjectionMessage> kafkaPersonConsumer,
            ILogger<Worker> logger)
        {
            _kafkaAccountConsumer = kafkaAccountConsumer;
            _kafkaPaymentConsumer = kafkaPaymentConsumer;
            _kafkaPersonConsumer = kafkaPersonConsumer;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _kafkaAccountConsumer.Consume(stoppingToken);
            _kafkaPaymentConsumer.Consume(stoppingToken);
            _kafkaPersonConsumer.Consume(stoppingToken);

            return Task.CompletedTask;
        }
    }
}
