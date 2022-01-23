using DataAccess.Elastic.Configure;
using DataAccess.Mongo.Configure;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Nest;
using Projector.Elastic.KafkaHandlers;
using Projector.Elastic.projections.Account;
using Projector.Elastic.projections.Payment;
using Projector.Elastic.projections.Person;
using Queries.Core.models;
using Settings;
using System;

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
                    services.AddSingleton<IMongoClient>(s =>
                        new MongoClient(MongoSettings.ConnectionString)
                    );

                    services.AddSingleton<IElasticClient>(s =>
                    {
                        var settings =
                            new ConnectionSettings(new Uri(ElasticSettings.Url))
                            .DefaultIndex(ElasticSettings.PersonsIndexName)
                            .DefaultMappingFor<Account>(m => m.IndexName(ElasticSettings.AccountsIndexName))
                            .DefaultMappingFor<Payment>(m => m.IndexName(ElasticSettings.PaymentsIndexName))
                            .DefaultMappingFor<Person>(m => m.IndexName(ElasticSettings.PersonsIndexName));

                        return new ElasticClient(settings);
                    });

                    services.AddKafkaConsumer<UpdateAccountProjectionMessage, UpdateAccountProjectionHandler>(
                        KafkaSettings.CommandTopics.AccountTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<UpdatePaymentProjectionMessage, UpdatePaymentProjectionHandler>(
                        KafkaSettings.CommandTopics.PaymentTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<UpdatePersonProjectionMessage, UpdatePersonProjectionHandler>(
                        KafkaSettings.CommandTopics.PersonTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaProducer<UpdateAccountProjectionMessage>(KafkaSettings.ProjectionTopics.AccountTopicName);
                    services.AddKafkaProducer<UpdatePaymentProjectionMessage>(KafkaSettings.ProjectionTopics.PaymentTopicName);
                    services.AddKafkaProducer<UpdatePersonProjectionMessage>(KafkaSettings.ProjectionTopics.PersonTopicName);

                    services.AddSingleton<IAccountProjector, AccountProjector>();
                    services.AddSingleton<IPaymentProjector, PaymentProjector>();
                    services.AddSingleton<IPersonProjector, PersonProjector>();

                    services.AddMongoDataAccessObjects();
                    services.AddElasticDataAccessObjects();

                    services.AddHostedService<Worker>();
                });
    }
}
