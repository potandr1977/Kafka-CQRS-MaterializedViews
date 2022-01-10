using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projector.Elastic.projections.Account;
using Projector.Elastic.projections.Payment;
using Projector.Elastic.projections.Person;
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
        private readonly IAccountProjector _accountProjector;
        private readonly IPaymentProjector _paymentProjector;
        private readonly IPersonProjector _personProjector;

        public Worker(
            IKafkaAccountConsumer kafkaAccountConsumer,
            IKafkaPaymentConsumer kafkaPaymentConsumer,
            IKafkaPersonConsumer kafkaPersonConsumer,
            IAccountProjector accountProjector,
            IPaymentProjector paymentProjector,
            IPersonProjector personProjector,
            ILogger<Worker> logger)
        {
            _logger = logger;
            
            _kafkaAccountConsumer = kafkaAccountConsumer;
            _kafkaPaymentConsumer = kafkaPaymentConsumer;
            _kafkaPersonConsumer = kafkaPersonConsumer;

            _accountProjector = accountProjector;
            _paymentProjector = paymentProjector;
            _personProjector = personProjector;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
            //accounts consumers
            var accountTask = _kafkaAccountConsumer.Consume(
                (key,value) => {
                    _accountProjector.ProjectOne(value);
                },
                0, null, stoppingToken);

            //payments consumers
            var paymentTask = _kafkaPaymentConsumer.Consume(
                (key, value) => {
                    _paymentProjector.ProjectOne(value);
                },
                stoppingToken);
            */
            //persons consumers
            var personTask = _kafkaPersonConsumer.Consume(
                (key, value) => {
                    _personProjector.ProjectOne(value);
                },
                (int) PartitionEnum.Second, null, stoppingToken);

            return Task.WhenAny(personTask);

            //return Task.WhenAny(accountTask, paymentTask, personTask);

        }
    }
}
