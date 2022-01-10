using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projector.Elastic.projections.Account;
using Projector.Elastic.projections.Payment;
using Projector.Elastic.projections.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projector.Elastic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IKafkaAccountConsumer, KafkaAccountConsumer>();
                    services.AddSingleton<IKafkaPaymentConsumer, KafkaPaymentConsumer>();
                    services.AddSingleton<IKafkaPersonConsumer, KafkaPersonConsumer>();

                    services.AddSingleton<IAccountProjector, AccountProjector>();
                    services.AddSingleton<IPaymentProjector, PaymentProjector>();
                    services.AddSingleton<IPersonProjector, PersonProjector>();

                    services.AddHostedService<Worker>();
                });
    }
}
