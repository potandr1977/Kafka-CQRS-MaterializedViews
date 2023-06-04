using DataAccess.Elastic.Configure;
using DataAccess.Mongo.Configure;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using Messages.Account;
using Messages.Payment;
using Messages.Person;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Nest;
using Projector.Elastic.projections.Account;
using Projector.Elastic.projections.Payment;
using Projector.Elastic.projections.Person;
using Queries.Core.models;
using Settings;
using SimpleViewProjector.Elastic.KafkaHandlers.Account;
using SimpleViewProjector.Elastic.KafkaHandlers.Payment;
using SimpleViewProjector.Elastic.KafkaHandlers.Person;
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
                    services.AddScoped<IMongoClient>(s =>
                        new MongoClient(MongoSettings.ConnectionString)
                    );

                    services.AddScoped<IElasticClient>(s =>
                    {
                        var settings =
                            new ConnectionSettings(new Uri(ElasticSettings.Url))
                            .DefaultIndex(ElasticSettings.PersonsIndexName)
                            .DefaultMappingFor<Account>(m => m.IndexName(ElasticSettings.AccountsIndexName))
                            .DefaultMappingFor<Payment>(m => m.IndexName(ElasticSettings.PaymentsIndexName))
                            .DefaultMappingFor<Person>(m => m.IndexName(ElasticSettings.PersonsIndexName));

                        return new ElasticClient(settings);
                    });
                    //Account
                    services.AddKafkaConsumer<UpdateAccountProjectionMessage, UpdateAccountProjectionHandler>(
                        KafkaSettings.CommandTopics.UpdateAccountTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<SaveAccountProjectionMessage, SaveAccountProjectionHandler>(
                        KafkaSettings.CommandTopics.SaveAccountTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<DeleteAccountProjectionMessage, DeleteAccountProjectionHandler>(
                        KafkaSettings.CommandTopics.DeleteAccountTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    //Payment
                    services.AddKafkaConsumer<UpdatePaymentProjectionMessage, UpdatePaymentProjectionHandler>(
                        KafkaSettings.CommandTopics.UpdatePaymentTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<SavePaymentProjectionMessage, SavePaymentProjectionHandler>(
                        KafkaSettings.CommandTopics.SavePaymentTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<DeletePaymentProjectionMessage, DeletePaymentProjectionHandler>(
                        KafkaSettings.CommandTopics.DeletePaymentTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    //Person
                    services.AddKafkaConsumer<UpdatePersonProjectionMessage, UpdatePersonProjectionHandler>(
                        KafkaSettings.CommandTopics.UpdatePersonTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<SavePersonProjectionMessage, SavePersonProjectionHandler>(
                        KafkaSettings.CommandTopics.SavePersonTopicName,
                        KafkaSettings.Groups.BusinessGroupId);

                    services.AddKafkaConsumer<DeletePersonProjectionMessage, DeletePersonProjectionHandler>(
                        KafkaSettings.CommandTopics.DeletePersonTopicName,
                        KafkaSettings.Groups.BusinessGroupId);


                    services.AddKafkaProducer<UpdateAccountProjectionMessage>(KafkaSettings.ProjectionTopics.UpdateAccountTopicName);
                    services.AddKafkaProducer<SaveAccountProjectionMessage>(KafkaSettings.ProjectionTopics.UpdateAccountTopicName);
                    services.AddKafkaProducer<DeleteAccountProjectionMessage>(KafkaSettings.ProjectionTopics.UpdateAccountTopicName);

                    services.AddKafkaProducer<UpdatePaymentProjectionMessage>(KafkaSettings.ProjectionTopics.UpdatePaymentTopicName);
                    services.AddKafkaProducer<SavePaymentProjectionMessage>(KafkaSettings.ProjectionTopics.SavePaymentTopicName);
                    services.AddKafkaProducer<DeletePaymentProjectionMessage>(KafkaSettings.ProjectionTopics.DeletePaymentTopicName);

                    services.AddKafkaProducer<UpdatePersonProjectionMessage>(KafkaSettings.ProjectionTopics.UpdatePersonTopicName);
                    services.AddKafkaProducer<SavePersonProjectionMessage>(KafkaSettings.ProjectionTopics.SavePersonTopicName);
                    services.AddKafkaProducer<DeletePersonProjectionMessage>(KafkaSettings.ProjectionTopics.DeletePersonTopicName);

                    services.AddScoped<IAccountProjector, AccountProjector>();
                    services.AddScoped<IPaymentProjector, PaymentProjector>();
                    services.AddScoped<IPersonProjector, PersonProjector>();

                    services.AddMongoDataAccessObjects();
                    services.AddElasticDataAccessObjects();

                    services.AddHostedService<Worker>();
                });
    }
}
