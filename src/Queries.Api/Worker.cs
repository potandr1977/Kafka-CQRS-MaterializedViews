using EventBus.Kafka.Abstraction;
using Messages.Account;
using Messages.Payments;
using Messages.Persons;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Api
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IKafkaConsumer<UpdateAccountProjectionMessage> _kafkaUpdateAccountConsumer;
        private readonly IKafkaConsumer<SaveAccountProjectionMessage> _kafkaSaveAccountConsumer;
        private readonly IKafkaConsumer<DeleteAccountProjectionMessage> _kafkaDeleteAccountConsumer;

        private readonly IKafkaConsumer<UpdatePaymentProjectionMessage> _kafkaUpdatePaymentConsumer;
        private readonly IKafkaConsumer<SavePaymentProjectionMessage> _kafkaSavePaymentConsumer;
        private readonly IKafkaConsumer<DeletePaymentProjectionMessage> _kafkaDeletePaymentConsumer;

        private readonly IKafkaConsumer<UpdatePersonProjectionMessage> _kafkaUpdatePersonConsumer;
        private readonly IKafkaConsumer<SavePersonProjectionMessage> _kafkaSavePersonConsumer;
        private readonly IKafkaConsumer<DeletePersonProjectionMessage> _kafkaDeletePersonConsumer;

        public Worker(
            IKafkaConsumer<UpdateAccountProjectionMessage> kafkaUpdateAccountConsumer,
            IKafkaConsumer<SaveAccountProjectionMessage> kafkaSaveAccountConsumer,
            IKafkaConsumer<DeleteAccountProjectionMessage> kafkaDeleteAccountConsumer,

            IKafkaConsumer<UpdatePaymentProjectionMessage> kafkaUpdatePaymentConsumer,
            IKafkaConsumer<SavePaymentProjectionMessage> kafkaSavePaymentConsumer,
            IKafkaConsumer<DeletePaymentProjectionMessage> kafkaDeletePaymentConsumer,

            IKafkaConsumer<UpdatePersonProjectionMessage> kafkaUpdatePersonConsumer,
            IKafkaConsumer<SavePersonProjectionMessage> kafkaSavePersonConsumer,
            IKafkaConsumer<DeletePersonProjectionMessage> kafkaDeletePersonConsumer,
            ILogger<Worker> logger)
        {
            _kafkaUpdateAccountConsumer = kafkaUpdateAccountConsumer;
            _kafkaSaveAccountConsumer = kafkaSaveAccountConsumer;
            _kafkaDeleteAccountConsumer = kafkaDeleteAccountConsumer;

            _kafkaUpdatePaymentConsumer = kafkaUpdatePaymentConsumer;
            _kafkaSavePaymentConsumer = kafkaSavePaymentConsumer;
            _kafkaDeletePaymentConsumer = kafkaDeletePaymentConsumer;

            _kafkaUpdatePersonConsumer = kafkaUpdatePersonConsumer;
            _kafkaSavePersonConsumer = kafkaSavePersonConsumer;
            _kafkaDeletePersonConsumer = kafkaDeletePersonConsumer;

            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _kafkaUpdateAccountConsumer.Consume(stoppingToken);
            _kafkaSaveAccountConsumer.Consume(stoppingToken);
            _kafkaDeleteAccountConsumer.Consume(stoppingToken);

            _kafkaUpdatePaymentConsumer.Consume(stoppingToken);
            _kafkaSavePaymentConsumer.Consume(stoppingToken);
            _kafkaDeletePaymentConsumer.Consume(stoppingToken);

            _kafkaUpdatePersonConsumer.Consume(stoppingToken);
            _kafkaSavePersonConsumer.Consume(stoppingToken);
            _kafkaDeletePersonConsumer.Consume(stoppingToken);

            return Task.CompletedTask;
        }
    }
}
