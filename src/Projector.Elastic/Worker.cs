using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Projector.Elastic
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IKafkaAccountConsumer _kafkaAccountConsumer;
        private readonly IKafkaPaymentConsumer _kafkaPaymentConsumer;
        private readonly IKafkaPersonConsumer _kafkaPersonConsumer;

        public Worker(
            IKafkaAccountConsumer kafkaAccountConsumer,
            IKafkaPaymentConsumer kafkaPaymentConsumer,
            IKafkaPersonConsumer kafkaPersonConsumer,
            ILogger<Worker> logger)
        {
            _logger = logger;
            
            _kafkaAccountConsumer = kafkaAccountConsumer;
            _kafkaPaymentConsumer = kafkaPaymentConsumer;
            _kafkaPersonConsumer = kafkaPersonConsumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //accounts consumers
            var accountTask = _kafkaAccountConsumer.Consume(
                (key,value) => {
                    var res = key;
                },
                0, null, stoppingToken);

            //payments consumers
            var paymentTask = _kafkaPaymentConsumer.Consume(
                (key, value) => {
                    var res = value;
                },
                stoppingToken);

            //persons consumers
            var personTask = _kafkaPersonConsumer.Consume(
                (key, value) => {
                    var res = value;
                },
                stoppingToken);

            return Task.WhenAny(accountTask, paymentTask, personTask);

        }
    }
}
